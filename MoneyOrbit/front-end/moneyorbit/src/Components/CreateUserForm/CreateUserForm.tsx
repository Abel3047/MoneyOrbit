import React, { useState } from 'react';

// Define the TypeScript type for the form's state.
// We can export this to use it in the parent component as well.
export interface UserCreationPayload {
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  accessLevel: string;
  email?: string;
  phoneNumber?: string;
}

// Define the props this component will accept from its parent.
interface UserCreationFormProps {
  onSubmit: (data: UserCreationPayload) => void; // A function to call with the form data
  isLoading: boolean;                           // A boolean to disable the button
}

export default function UserCreationForm({ onSubmit, isLoading }: UserCreationFormProps) {
    // State for the form fields remains the same.
    const [formData, setFormData] = useState<UserCreationPayload>({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
        accessLevel: 'User',
        email: '',
        phoneNumber: ''
    });

    // The handleChange function also remains the same.
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    // The handleSubmit function is now much simpler.
    // It prevents the default browser action and calls the function passed down from the parent.
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit(formData); // Pass the form data up to the parent component.
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-6 border rounded-lg shadow-md max-w-lg mx-auto">
            <h2 className="text-2xl font-semibold text-center">Create New User</h2>
            
            {/* The JSX for the form fields is identical */}
            <div>
                <label className="block text-sm font-medium text-gray-700">Username</label>
                <input type="text" name="userName" value={formData.userName} onChange={handleChange} className="w-full border p-2 rounded mt-1" required />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Password</label>
                <input type="password" name="password" value={formData.password} onChange={handleChange} className="w-full border p-2 rounded mt-1" required />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">First Name</label>
                <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} className="w-full border p-2 rounded mt-1" required />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Last Name</label>
                <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} className="w-full border p-2 rounded mt-1" required />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Access Level</label>
                <select name="accessLevel" value={formData.accessLevel} onChange={handleChange} className="w-full border p-2 rounded mt-1 bg-white" required>
                    <option value="User">User</option>
                    <option value="Administrative">Administrator</option>
                </select>
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Email Address (Optional)</label>
                <input type="email" name="email" value={formData.email} onChange={handleChange} className="w-full border p-2 rounded mt-1" />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Phone Number (Optional)</label>
                <input type="tel" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} className="w-full border p-2 rounded mt-1" />
            </div>
            
            {/* The button is now disabled based on the isLoading prop */}
            <button
                type="submit"
                disabled={isLoading}
                className="w-full bg-blue-600 text-white p-2 rounded hover:bg-blue-700 transition-colors disabled:bg-gray-400"
            >
                {isLoading ? 'Creating User...' : 'Create User'}
            </button>
        </form>
    );
}