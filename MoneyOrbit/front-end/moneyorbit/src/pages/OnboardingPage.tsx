import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import CreateUserForm from '../Components/CreateUserForm/CreateUserForm';
import CreateAccountForm from '../Components/CreateAccountForm/CreateAccountForm';
import CreateGoalsForm from '../Components/CreateGoalsForm/CreateGoalsForm';
import {CreateAccountDto, CreateGoalsDto} from '../Models/Dtos';
import { UserCreationDto } from '../Models/Dtos';

interface CreateUserCredentials {
    //Variables from DTO
}
interface CreateAccountCredentials {
    //Variables from DTO
    token: string;

    accountName: string;
    description?: string;

    isAsset: boolean;
    isExpense: boolean;
    isCaptial: boolean;
    isLiability: boolean;

    // BankAccount creation properties
    // Accounts are typically not bank accounts so the default is false
    isBankAccount?: boolean;

    bankAccountName?: string;
    bankAccountNumber?: string;
    bankBranchCode?: string;
    bankBranchName?: string;
    bankSwiftCode?: string;
}
interface CreateGoalsCredentials {
    //Variables from DTO
    date: string;
    goalName: string;
    goalDescription: string;
    accDebitedID: string;
    amount: number;
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
                return <CreateAccountForm onCreateAccount={handleAccountCreation} />;
            case 'createGoals':
                return <CreateGoalsForm onCreateGoal={handleGoalCreation} />;
            default:
                return <CreateUserForm />; // Fallback to the default form
        }
    };

    // Simple CSS-in-JS for active tab styling
    const activeTabStyle = 'bg-blue-500 text-white';
    const inactiveTabStyle = 'bg-gray-200 text-black';

    const navigate = useNavigate();

    /**
 * Handles the user registration process. This function acts as the "middleware"
 * between the registration form and the backend API.
 * 
 * @param userData - An object containing all necessary fields for user creation.
 */
const handleUserRegisteration = async (userData: UserCreationDto) => {
    console.log("Attempting to register user with data:", userData);

    // --- MAPPING STEP ---
    // This is the crucial part. We create a payload object where the keys
    // EXACTLY match the property names in your C# UserCreationDto, including casing.
    const payload = {
        UserName: userData.UserName,
        password: userData.password, // Your C# DTO has a lowercase 'p'
        FirstName: userData.FirstName,
        LastName: userData.LastName,
        AccessLevel: userData.AccessLevel,
        Email: userData.Email || null, // Send null if the string is empty
        PhoneNumber: userData.PhoneNumber || null,
    };

    // --- API CALL LOGIC ---
    try {
        console.log("Sending payload to API:", payload);

        // Your C# code probably returns a ResultObject like { result: "someUserId", error: null }
        // We define the expected response shape for type safety

        const response = await axios.post(baseAPIPath + 'Auth/register', payload);

        // Check the response from your .NET API
        if (response.data && response.data.error) {
            // Handle specific errors returned from the API
            throw new Error(response.data.error);
        }

        if (response.data && response.data.result) {
            // If the API call is successful:
            console.log("Registration successful! User ID:", response.data.result);
            alert('Registration successful! Please log in.');

            // Navigate the user to the login page, not the dashboard
            navigate('/login');
        } else {
            // Handle unexpected successful responses that don't match the expected shape
            throw new Error("Received an invalid response from the server.");
        }

    } catch (error) {
        // This block catches network errors or errors thrown from the try block
        const errorMessage = (error as any).response?.data?.message || (error as Error).message || "An unknown error occurred.";
        console.error('Registration failed:', errorMessage);
        alert(`Registration failed: ${errorMessage}`);
    }
};
    // The handleAccountCreation function. This is the "middleware" logic
    const handleAccountCreation = async (credentials: CreateAccountCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("OnboardingPage received account creation credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: CreateAccountDto = {
            token: credentials.token,
            accountName: credentials.accountName,
            isAsset: credentials.isAsset,
            isExpense: credentials.isExpense,
            isCaptial: credentials.isCaptial,
            isLiability: credentials.isLiability
        };

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
        const payload: CreateGoalsDto = {
            date: credentials.date,
            goalName: credentials.goalName,
            goalDescription: credentials.goalDescription,
            accDebitedID: credentials.accDebitedID,
            amount: credentials.amount
        };

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
    const navigateDashboard = () => {
        navigate('/dashboard');
    }
    return (
        <div className="container mx-auto p-8 max-w-2xl">
            <h1 className="text-3xl font-bold mb-6">Onboarding</h1>

            {/* 3. Navigation to switch between forms */}
            <div className="flex border-b mb-6">
                {/* 4. Buttons to switch between forms */}
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

            <div>
                {renderActiveForm()}
            </div>

            <div>
                <button
                    onClick={navigateDashboard}
                    className={`py-2 px-4 ${activeForm === 'createGoals' ? activeTabStyle : inactiveTabStyle}`}
                >
                    Go to Dashboard
                </button>
            </div>
        </div>
    );
}