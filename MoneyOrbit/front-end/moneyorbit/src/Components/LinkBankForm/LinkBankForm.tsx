import React, { useState } from 'react';

// Export the DTO type so the parent page can use it for its state and handlers.
export interface LinkBankAccountDto {
  bankAccountName: string;
  bankAccountNumber: string;
  bankBranchCode: string;
  bankBranchName: string;
  bankSwiftCode?: string;
}

// --- THIS IS THE INTERFACE THAT NEEDS TO BE FIXED ---
// Define all the props this component expects to receive from its parent.
interface LinkBankFormProps {
  onSubmit: (data: LinkBankAccountDto) => void;
  isLoading: boolean;
  error: string | null;            // <-- ADD THIS PROPERTY
  successMessage: string | null;   // <-- ADD THIS PROPERTY
}

// --- UPDATE THE FUNCTION SIGNATURE TO ACCEPT THE NEW PROPS ---
export default function LinkBankForm({ onSubmit, isLoading, error, successMessage }: LinkBankFormProps) {
    
    const [formData, setFormData] = useState<LinkBankAccountDto>({
        bankAccountName: '',
        bankAccountNumber: '',
        bankBranchCode: '',
        bankBranchName: '',
        bankSwiftCode: '',
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        onSubmit(formData);
    };

    return (
        // The form now includes the layout and the display for the messages it receives.
        <div className="container mx-auto p-8 max-w-2xl">
            <h1 className="text-3xl font-bold mb-6 text-center">Link your Bank Account</h1>
            
            {/* --- USE THE NEW PROPS TO DISPLAY MESSAGES --- */}
            {error && <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4" role="alert">{error}</div>}
            {successMessage && <div className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded mb-4" role="alert">{successMessage}</div>}

            <div className="bg-white p-6 rounded-lg shadow-md">
                <form onSubmit={handleSubmit} className="space-y-4">
                    {/* ... your form JSX ... */}
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Bank Account Name</label>
                        <input type="text" name="bankAccountName" value={formData.bankAccountName} onChange={handleChange} className="mt-1 w-full border p-2 rounded" required />
                    </div>
                     <div>
                        <label className="block text-sm font-medium text-gray-700">Bank Account Number</label>
                        <input type="text" name="bankAccountNumber" value={formData.bankAccountNumber} onChange={handleChange} className="mt-1 w-full border p-2 rounded" required />
                    </div>
                     <div>
                        <label className="block text-sm font-medium text-gray-700">Bank Branch Code</label>
                        <input type="text" name="bankBranchCode" value={formData.bankBranchCode} onChange={handleChange} className="mt-1 w-full border p-2 rounded" required />
                    </div>
                     <div>
                        <label className="block text-sm font-medium text-gray-700">Bank Branch Name</label>
                        <input type="text" name="bankBranchName" value={formData.bankBranchName} onChange={handleChange} className="mt-1 w-full border p-2 rounded" required />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700">Bank SWIFT Code (Optional)</label>
                        <input type="text" name="bankSwiftCode" value={formData.bankSwiftCode || ''} onChange={handleChange} className="mt-1 w-full border p-2 rounded" />
                    </div>
                    <button
                        type="submit"
                        disabled={isLoading}
                        className="w-full bg-blue-600 text-white p-3 rounded hover:bg-blue-700 disabled:bg-gray-400"
                    >
                        {isLoading ? 'Linking...' : 'Link Account'}
                    </button>
                </form>
            </div>
        </div>
    );
}