import React, { useState } from 'react';

// Export the DTO type so the parent can use it
export interface LinkBankAccountDto {
  bankAccountName: string;
  bankAccountNumber: string;
  bankBranchCode: string;
  bankBranchName: string;
  bankSwiftCode?: string;
}

// Define the props this component expects to receive.
interface LinkBankFormProps {
  onSubmit: (data: LinkBankAccountDto) => void;
  isLoading: boolean;
}

// Apply the props type here. This is where it belongs.
export default function LinkBankForm({ onSubmit, isLoading }: LinkBankFormProps) {
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
        onSubmit(formData); // Pass the data up to the parent via the prop function
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4 border rounded-lg shadow-md">
            {/* The rest of your JSX remains the same */}
            <div>
                <label className="block text-sm font-medium text-gray-700">Bank Account Name</label>
                <input
                    type="text"
                    name="bankAccountName"
                    value={formData.bankAccountName}
                    onChange={handleChange}
                    className="mt-1 w-full border p-2 rounded"
                    placeholder="e.g., My Checking Account"
                    required
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Bank Account Number</label>
                <input
                    type="text"
                    name="bankAccountNumber"
                    value={formData.bankAccountNumber}
                    onChange={handleChange}
                    className="mt-1 w-full border p-2 rounded"
                    placeholder="e.g., 1234567890"
                    required
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Bank Branch Code</label>
                <input
                    type="text"
                    name="bankBranchCode"
                    value={formData.bankBranchCode}
                    onChange={handleChange}
                    className="mt-1 w-full border p-2 rounded"
                    placeholder="e.g., 9876"
                    required
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Bank Branch Name</label>
                <input
                    type="text"
                    name="bankBranchName"
                    value={formData.bankBranchName}
                    onChange={handleChange}
                    className="mt-1 w-full border p-2 rounded"
                    placeholder="e.g., Downtown Main Branch"
                    required
                />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Bank SWIFT Code (Optional)</label>
                <input
                    type="text"
                    name="bankSwiftCode"
                    value={formData.bankSwiftCode || ''}
                    onChange={handleChange}
                    className="mt-1 w-full border p-2 rounded"
                    placeholder="e.g., BANKUS33"
                />
            </div>
            <button
                type="submit"
                disabled={isLoading}
                className="w-full bg-blue-600 text-white p-3 rounded hover:bg-blue-700 disabled:bg-gray-400"
            >
                {isLoading ? 'Linking...' : 'Link Account'}
            </button>
        </form>
    );
}