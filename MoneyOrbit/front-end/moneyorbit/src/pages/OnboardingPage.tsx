import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import CreateUserForm, { UserCreationPayload } from '../Components/CreateUserForm/CreateUserForm';
import CreateAccountForm from '../Components/CreateAccountForm/CreateAccountForm';
import CreateGoalsForm from '../Components/CreateGoalsForm/CreateGoalsForm';

// These interfaces are placeholders. You should define them based on your DTOs/form needs.
interface CreateAccountCredentials {
    // Variables for account creation
}
interface CreateGoalsCredentials {
    // Variables for goal creation
}

// Defines the possible forms that can be used in the onboarding process
type ActiveForm = 'createUser' | 'createAccount' | 'createGoals';

export default function OnboardingPage() {
    const navigate = useNavigate();

    // State to track the currently active form.
    const [activeForm, setActiveForm] = useState<ActiveForm>('createUser');
    
    // IMPROVEMENT: State to manage the loading status of API calls.
    const [isLoading, setIsLoading] = useState(false);

    /**
     * Handles the user registration process. It's called when the CreateUserForm is submitted.
     * @param formData - The data object directly from the UserCreationForm component (camelCase).
     */
    const handleUserRegisteration = async (formData: UserCreationPayload) => {
        console.log("Attempting to register user with form data:", formData);
        setIsLoading(true); // Disable the form button

        // --- MAPPING STEP ---
        // Convert the form's camelCase data to the PascalCase DTO the C# API expects.
        // Double-check your C# DTO for the exact property names (especially 'password' vs 'Password').
        const payload = {
            UserName: formData.userName,
            Password: formData.password, // IMPORTANT: Ensure this matches your C# DTO property name exactly.
            FirstName: formData.firstName,
            LastName: formData.lastName,
            AccessLevel: formData.accessLevel,
            Email: formData.email || null, // Send null if the string is empty
            PhoneNumber: formData.phoneNumber || null,
        };

        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'User/RegisterUser', payload);

            if (response.data && response.data.result) {
                console.log("Registration successful! User ID:", response.data.result);
                alert('User created successfully! Please create an account.');
                
                // IMPROVEMENT: Instead of navigating away, move to the next onboarding step.
                setActiveForm('createAccount');
            } else {
                // Handle cases where the API returns an error in its standard response shape.
                throw new Error(response.data.error || "An unknown error occurred during registration.");
            }
        } catch (error) {
            // This block catches network errors or errors thrown from the try block.
            const errorMessage = (error as any).response?.data?.message || (error as Error).message;
            console.error('Registration failed:', errorMessage);
            alert(`Registration failed: ${errorMessage}`);
        } finally {
            setIsLoading(false); // Re-enable the form button, whether it succeeded or failed.
        }
    };

    // Placeholder for account creation logic.
    const handleAccountCreation = async (credentials: CreateAccountCredentials) => {
        console.log("Account creation triggered with:", credentials);
        // TODO: Implement API call with loading state, mapping, and error handling.
        alert("Account creation logic not implemented yet.");
    };

    // Placeholder for goal creation logic.
    const handleGoalCreation = async (credentials: CreateGoalsCredentials) => {
        console.log("Goal creation triggered with:", credentials);
        // TODO: Implement API call with loading state, mapping, and error handling.
        alert("Goal creation successful! Navigating to dashboard.");
        // After the FINAL step, you navigate the user away.
        navigate('/dashboard');
    };

    // --- CRITICAL FIX ---
    // The stray `navigate('/dashboard')` call has been REMOVED from here.
    // It should only be called inside a handler after an action is complete.

    // This helper function renders the correct form based on the `activeForm` state.
    const renderActiveForm = () => {
        switch (activeForm) {
            case 'createUser':
                // FIX: Pass the REAL handler function and loading state to the form component.
                return <CreateUserForm onSubmit={handleUserRegisteration} isLoading={isLoading} />;
            
            case 'createAccount':
                // You will need to wire this up similarly to CreateUserForm
                return <CreateAccountForm /* onSubmit={handleAccountCreation} isLoading={isLoading} */ />;
            
            case 'createGoals':
                 // And this one too
                return <CreateGoalsForm /* onSubmit={handleGoalCreation} isLoading={isLoading} */ />;
            
            default:
                // Fallback to the default form
                return <CreateUserForm onSubmit={handleUserRegisteration} isLoading={isLoading} />;
        }
    };

    const activeTabStyle = 'bg-blue-500 text-white';
    const inactiveTabStyle = 'bg-gray-200 text-black';

    return (
        <div className="container mx-auto p-8 max-w-2xl">
            <h1 className="text-3xl font-bold mb-6">Onboarding</h1>

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

            <div>
                {renderActiveForm()}
            </div>
        </div>
    );
}