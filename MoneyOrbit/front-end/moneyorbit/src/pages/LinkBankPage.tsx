import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import LinkBankForm from '../Components/LinkBankForm/LinkBankForm';

interface LinkBankCredentials {
    //Variables from DTO
}

export default function LinkBankPage() {

    // 1. State to track the currently active form. Default to 'profile'.
    const [activeForm, setActiveForm] = useState('linkBank');
    
    const navigate = useNavigate();

    // The handleUserRegistration function. This is the "middleware" logic
    const handleBankRegisteration = async (credentials: LinkBankCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("Bank Registration received bank registeration credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: any = null;

        // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'Account/LinkBankAccount', payload);

            // If the API call is successful:
            console.log(response.data);
            alert('Registration successful!');

            // Navigate the user to the dashboard page
            navigate('/dashboard');

        } catch (error) {
            console.error('Registration failed:', error);
            alert('Registration failed. Please check your credentials.');
        }
    };

    // Navigate the user to the dashboard page
    navigate('/dashboard');

    return (
        <div className="container mx-auto p-8 max-w-2xl">
            <h1 className="text-3xl font-bold mb-6">Link your Bank Account</h1>

            {/* 3. Navigation to switch between forms */}
            <div className="flex border-b mb-6">
                
                </div>

           <LinkBankForm />
        </div>
    );
}