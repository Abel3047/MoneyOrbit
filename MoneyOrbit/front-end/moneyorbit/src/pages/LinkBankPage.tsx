import React, { useState } from 'react';
import axios from 'axios';
import { baseAPIPath } from '../services/baseServices';
// Import the dumb form component AND its data shape type
import LinkBankForm, { LinkBankAccountDto } from '../Components/LinkBankForm/LinkBankForm';

export default function LinkBankPage() {
  // State for managing API call status and messages
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  // This is the "middleware" logic, now with an explicit mapping step.
  const handleLinkBankAccount = async (credentials: LinkBankAccountDto) => {
    setIsLoading(true);
    setError(null);
    setSuccessMessage(null);

    const token = localStorage.getItem('authToken');
    if (!token) {
        setError('Authentication error. Please log in again.');
        setIsLoading(false);
        return;
    }

    // --- MAPPING STEP ---
    // Create the payload object with keys that EXACTLY match your C# LinkBankAccountDto.
    // This is the core of the refactor.
    const apiPayload = {
        BankAccountName: credentials.bankAccountName,
        BankAccountNumber: credentials.bankAccountNumber,
        BankBranchCode: credentials.bankBranchCode,
        BankBranchName: credentials.bankBranchName,
        BankSwiftCode: credentials.bankSwiftCode || null, // Send null if optional field is empty
    };

    try {
      const endpoint = 'User/LinkBankAccount'; // Ensure this matches your API route
      
      console.log("Sending payload to API:", apiPayload);

      // --- API CALL ---
      // Send the correctly shaped `apiPayload` to the backend.
      const response = await axios.post(baseAPIPath + endpoint, apiPayload, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${token}`
          }
      });

      // The backend returns a simple string on success in this case
      setSuccessMessage(response.data); 

    } catch (err: any) {
      const errorMessage = err.response?.data?.message || err.response?.data || 'An unexpected error occurred.';
      setError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  };

  // The Page component now just renders the Form, passing all necessary state and handlers.
  // This makes its structure identical to your LoginPage example.
  return (
    <LinkBankForm 
      onSubmit={handleLinkBankAccount} 
      isLoading={isLoading}
      error={error}
      successMessage={successMessage}
    />
  );
}