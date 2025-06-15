import React, { useState } from 'react';
import axios from 'axios';
import { baseAPIPath } from '../services/baseServices'; // Adjust this import path as necessary
import LinkBankForm from '../Components/LinkBankForm/LinkBankForm';

// TypeScript interface to define the shape of our data, matching the C# DTO
interface LinkBankAccountDto {
  bankAccountName: string;
  bankAccountNumber: string;
  bankBranchCode: string;
  bankBranchName: string;
  bankSwiftCode?: string;
}

export default function LinkBankPage() {
  // State for managing API call status
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  // This is your "middleware" logic to handle the API call
  const handleLinkBankAccount = async (credentials: LinkBankAccountDto) => {
    setIsLoading(true);
    setError(null);
    setSuccessMessage(null);

    // IMPORTANT: Get the authentication token from storage.
    // Linking a bank account must be a protected action.
    const token = localStorage.getItem('authToken');
    if (!token) {
        setError('Authentication error. Please log in again.');
        setIsLoading(false);
        return;
    }

    try {
      console.log("Sending payload to API:", credentials);

      // The backend endpoint route for linking an account is needed here.
      // Based on your previous context, let's assume it's `/User/link-bank-account`
      // You must update this to match your actual controller route.
      const endpoint = 'User/link-bank-account'; 
      
      const response = await axios.post(baseAPIPath + endpoint, credentials, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${token}` // Send the token for authentication
          }
      });

      // If the API call is successful, set the success message from the API response
      console.log("API Response:", response.data);
      setSuccessMessage(response.data); // Your API returns a success string directly

    } catch (err: any) {
      console.error('Failed to link bank account:', err);
      // Extract the error message from the API response if it exists
      const errorMessage = err.response?.data || 'An unexpected error occurred. Please try again.';
      setError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="container mx-auto p-8 max-w-2xl">
      <h1 className="text-3xl font-bold mb-6 text-center">Link your Bank Account</h1>
      
      {/* Conditionally render messages */}
      {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative mb-4" role="alert">{error}</div>}
      {successMessage && <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded relative mb-4" role="alert">{successMessage}</div>}

      {/* Render the form, passing the handler function and loading state as props */}
      <div className="bg-white p-6 rounded-lg shadow-md">
        <LinkBankForm onSubmit={handleLinkBankAccount} isLoading={isLoading} />
      </div>
    </div>
  );
}