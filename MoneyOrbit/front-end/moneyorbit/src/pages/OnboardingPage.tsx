import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import CreateUserForm from '../Components/CreateUserForm/CreateUserForm';
import CreateAccountForm from '../Components/CreateAccountForm/CreateAccountForm';
import CreateGoalsForm from '../Components/CreateGoalsForm/CreateGoalsForm';

interface CreateUserCredentials {
    //Variables from DTO
}
interface CreateAccountCredentials {
    //Variables from DTO
}
interface CreateGoalsCredentials {
    //Variables from DTO
}

//Define the possible forms that can be used in the onboarding process
type ActiveForm = 'createUser' | 'createAccount' | 'createGoals';

export default function OnboardingPage() {

    // 1. State to track the currently active form. Default to 'profile'.
    const [activeForm, setActiveForm] = useState<ActiveForm>('createUser');

    // 2. A helper function for conditional rendering (or you can do it inline)
    const renderActiveForm = () => {
        switch (activeForm) {
            case 'createUser':
                return <CreateUserForm />;
            case 'createAccount':
                return <CreateAccountForm />;
            case 'createGoals':
                return <CreateGoalsForm />;
            default:
                return <CreateUserForm />; // Fallback to the default form
        }
    };

    // Simple CSS-in-JS for active tab styling
    const activeTabStyle = 'bg-blue-500 text-white';
    const inactiveTabStyle = 'bg-gray-200 text-black';

    const navigate = useNavigate();

    // The handleUserRegistration function. This is the "middleware" logic
    const handleUserRegisteration = async (credentials: CreateUserCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("OnboardingPage received user registeration credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: any = null;

        // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'User/RegisterUser', payload);

            // If the API call is successful:
            console.log(response.data);
            alert('Login successful!');

            // Navigate the user to the dashboard page
            navigate('/dashboard');

        } catch (error) {
            console.error('Login failed:', error);
            alert('Login failed. Please check your credentials.');
        }
    };
    // The handleAccountCreation function. This is the "middleware" logic
    const handleAccountCreation = async (credentials: CreateAccountCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("OnboardingPage received account creation credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: any = null;

        // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'Account/CreateAccount', payload);

            // If the API call is successful:
            console.log(response.data);
            alert('Account created successfully!');

            // Navigate the user to the dashboard page
            navigate('/dashboard');

        } catch (error) {
            console.error('Account creation failed:', error);
            alert('Account creation failed. Please check your credentials.');
        }
    };
    // The handleUserRegistration function. This is the "middleware" logic
    const handleGoalCreation = async (credentials: CreateGoalsCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("OnboardingPage received goal creation credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: any = null;

        // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'Goal/CreateGoal', payload);

            // If the API call is successful:
            console.log(response.data);
            alert('Goal created successfully!');

            // Navigate the user to the dashboard page
            navigate('/dashboard');

        } catch (error) {
            console.error('Goal creation failed:', error);
            alert('Goal creation failed. Please check your credentials.');
        }
    };


    // Navigate the user to the dashboard page
    navigate('/dashboard');

    return (
        <div className="container mx-auto p-8 max-w-2xl">
            <h1 className="text-3xl font-bold mb-6">User Settings</h1>

            {/* 3. Navigation to switch between forms */}
            <div className="flex border-b mb-6">
                <button
                    onClick={() => setActiveForm('createUser')}
                    className={`py-2 px-4 ${activeForm === 'createUser' ? activeTabStyle : inactiveTabStyle}`}
                >
                    Create User
                </button>
                <button
                    onClick={() => setActiveForm('createAccount')}
                    className={`py-2 px-4 ${activeForm === 'createAccount' ? activeTabStyle : inactiveTabStyle}`}
                >
                    Create Account
                </button>
                <button
                    onClick={() => setActiveForm('createGoals')}
                    className={`py-2 px-4 ${activeForm === 'createGoals' ? activeTabStyle : inactiveTabStyle}`}
                >
                    Create Goals
                </button>
            </div>

            {/* 4. Render the active form component */}
            <div>
                {renderActiveForm()}
            </div>
        </div>
    );
}