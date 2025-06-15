import React, { useState } from 'react';

export default function LinkBankForm() {
    const [formData, setFormData] = useState({
        firstName: 'Abel',
        lastName: 'T',
        email: 'abel@example.com'
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Submitting Profile Data:", formData);
        try {
            // const response = await axios.post('/api/user/profile', formData);
            alert('Profile updated successfully!');
        } catch (error) {
            console.error("Failed to update profile", error);
            alert('Failed to update profile.');
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4 border rounded-lg">
            <h2 className="text-xl font-semibold">Edit Profile</h2>
            <div>
                <label>First Name</label>
                <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} className="w-full border p-2 rounded" />
            </div>
            <div>
                <label>Last Name</label>
                <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} className="w-full border p-2 rounded" />
            </div>
            <div>
                <label>Email Address</label>
                <input type="email" name="email" value={formData.email} onChange={handleChange} className="w-full border p-2 rounded" />
            </div>
            <button type="submit" className="bg-blue-500 text-white p-2 rounded">Save Profile</button>
        </form>
    );
}