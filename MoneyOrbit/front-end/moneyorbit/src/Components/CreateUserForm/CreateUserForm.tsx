import React, { useState } from 'react';
import axios from 'axios'; // For making API requests

// Define the TypeScript type that matches your C# UserCreationDto
// This provides type safety and autocompletion for your form data.
interface UserCreationPayload {
  userName: string;
  password: string; // Lowercase 'p' to match your DTO
  firstName: string;
  lastName: string;
  accessLevel: string;
  email?: string; // Optional fields are marked with '?'
  phoneNumber?: string;
}

export default function UserCreationForm() {
    // Initialize the state with all the fields required by the DTO
    const [formData, setFormData] = useState<UserCreationPayload>({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
        accessLevel: 'User', // Set a default value for the access level
        email: '',
        phoneNumber: ''
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    /**
     * This function handles the form submission. It acts as the "middleware"
     * by mapping the React state to the C# DTO shape and sending it to the API.
     */
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        
        // --- MAPPING STEP ---
        // Create the payload object with keys that EXACTLY match your C# DTO
        const apiPayload = {
            UserName: formData.userName,
            password: formData.password, // Lowercase 'p'
            FirstName: formData.firstName,
            LastName: formData.lastName,
            AccessLevel: formData.accessLevel,
            Email: formData.email,
            PhoneNumber: formData.phoneNumber,
        };

        console.log("Submitting User Creation Data:", apiPayload);

        try {
            // Send the mapped payload to your .NET registration endpoint
            const response = await axios.post('/api/auth/register', apiPayload);

            alert('User created successfully! Response: ' + JSON.stringify(response.data));
            // Reset the form after successful submission
            setFormData({
                userName: '', password: '', firstName: '', lastName: '',
                accessLevel: 'User', email: '', phoneNumber: ''
            });

        } catch (error) {
            console.error("Failed to create user", error);
            // Try to show a more specific error message from the backend if available
            const errorMessage = (error as any).response?.data?.message || "An unknown error occurred.";
            alert(`Failed to create user: ${errorMessage}`);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-6 border rounded-lg shadow-md max-w-lg mx-auto">
            <h2 className="text-2xl font-semibold text-center">Create New User</h2>
            
            {/* Required Fields */}
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

            {/* Optional Fields */}
            <div>
                <label className="block text-sm font-medium text-gray-700">Email Address (Optional)</label>
                <input type="email" name="email" value={formData.email} onChange={handleChange} className="w-full border p-2 rounded mt-1" />
            </div>
            <div>
                <label className="block text-sm font-medium text-gray-700">Phone Number (Optional)</label>
                <input type="tel" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} className="w-full border p-2 rounded mt-1" />
            </div>
            
            <button type="submit" className="w-full bg-blue-600 text-white p-2 rounded hover:bg-blue-700 transition-colors">
                Submit
            </button>
        </form>
    );
}