import React, { useState } from 'react';
import axios from 'axios';
import { baseAPIPath } from '../services/baseServices';
// Import the dumb form component AND the type for its data
import LinkBankForm, { LinkBankAccountDto } from '../Components/LinkBankForm/LinkBankForm';

// REMOVE the props from the function signature. This is the main fix.
export default function LinkBankPage() {
  // --- This is where the state and logic should live ---
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

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

    try {
      const endpoint = 'User/link-bank-account'; // Make sure this matches your API route
      
      const response = await axios.post(baseAPIPath + endpoint, credentials, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${token}`
          }
      });

      setSuccessMessage(response.data.message || "Bank account linked successfully!");

    } catch (err: any) {
      const errorMessage = err.response?.data?.message || 'An unexpected error occurred. Please try again.';
      setError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="container mx-auto p-8 max-w-2xl">
      <h1 className="text-3xl font-bold mb-6 text-center">Link your Bank Account</h1>
      
      {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4" role="alert">{error}</div>}
      {successMessage && <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded mb-4" role="alert">{successMessage}</div>}

      <div className="bg-white p-6 rounded-lg shadow-md">
        {/* The parent renders the child and PASSES the state and logic DOWN as props */}
        <LinkBankForm onSubmit={handleLinkBankAccount} isLoading={isLoading} />
      </div>
    </div>
  );
}