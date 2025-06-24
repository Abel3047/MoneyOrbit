import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import CreateAccountForm from '../Components/CreateAccountForm/CreateAccountForm';
import { CreateAccountCredentials } from './OnboardingPage';
import { BaseRegisterBankAccountDto } from '../Models/Dtos';
import { getAuthToken } from '../services/PersistenceServices';

export default function LinkBankPage() {    
    const navigate = useNavigate();

    // The handleUserRegistration function. This is the "middleware" logic
    const handleBankRegisteration = async (credentials: CreateAccountCredentials) => {
        // credentials will be an object like { username: 'user123', password: '...' }
        console.log("Bank Registration received bank registeration credentials:", credentials);

        // This is the mapping step. You convert the data from the form's shape
        // to the exact shape the API requires.
        const payload: BaseRegisterBankAccountDto = {
                    token: await getAuthToken() || '',
                    AccountName: credentials.accountName ?? '',
                    description: credentials.description,     
                    BankAccountName: credentials.bankAccountName ?? '',
                    BankAccountNumber: credentials.bankAccountNumber ?? '',   
                    BankBranchName: credentials.bankBranchName ?? '',
                    BankBranchCode: credentials.bankBranchCode ?? '',
                    BankSwiftCode: credentials.bankSwiftCode               
                };
        // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
        try {
            console.log("Sending payload to API:", payload);
            const response = await axios.post(baseAPIPath + 'Account/RegisterWithAccountNumber', payload);

            // If the API call is successful:
            console.log(response.data);
            alert('Bank Registration successful!');

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
            <h1 className="text-3xl font-bold mb-6">Link Your Bank Account</h1>
            <div>
                <CreateAccountForm onCreateAccount={handleBankRegisteration} isBankAccount={true}/>
            </div>
        </div>
    
    );
}