import React, { useState } from 'react';
import {CreateAccountDto} from '../../Models/Dtos';


interface CreateAccountProps {
    onCreateAccount: (credentials: CreateAccountDto) => void;
}

const CreateAccountForm: React.FC<CreateAccountProps> = ({ onCreateAccount }) => {

    const [token, setToken] = useState('');
    const [accountName, setAccountName] = useState('');
    const [description, setDescription] = useState<string | undefined>(undefined);

    const [isAsset, setIsAsset] = useState(false);
    const [isExpense, setIsExpense] = useState(false);
    const [isCaptial, setIsCaptial] = useState(false);
    const [isLiability, setIsLiability] = useState(false);

    const [isBankAccount, setIsBankAccount] = useState(false);

    const [bankAccountName, setBankAccountName] = useState<string | undefined>(undefined);
    const [bankAccountNumber, setBankAccountNumber] = useState<string | undefined>(undefined);
    const [bankBranchCode, setBankBranchCode] = useState<string | undefined>(undefined);
    const [bankBranchName, setBankBranchName] = useState<string | undefined>(undefined);
    const [bankSwiftCode, setBankSwiftCode] = useState<string | undefined>(undefined);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        onCreateAccount({
            token,
            accountName,
            description,
            isAsset,
            isExpense,
            isCaptial,
            isLiability,
            isBankAccount,
            bankAccountName,
            bankAccountNumber,
            bankBranchCode,
            bankBranchName,
            bankSwiftCode,
        });
    };


    return (
            <div className="wrapper">
                <form onSubmit={handleSubmit}>
                    <h1>Sign Up</h1>
                    <div className="input-box">
                        <input type="text"
                            placeholder="token"
                            required value={token} onChange={(e) => setToken(e.target.value)} />
                    </div>
                    <div className="input-box">
                        <input
                            type="text"
                            placeholder="accountName"
                            required
                            // Connect input value to state
                            value={accountName}
                            // Update state when user types
                            onChange={(e) => setAccountName(e.target.value)}
                        />
                    </div>
                    <div className="input-box">
                        <label htmlFor="accountType">Account Type</label>
                        <select
                            id="accountType"
                            onChange={(e) => {
                            // Reset all types
                            setIsAsset(false);
                            setIsExpense(false);
                            setIsCaptial(false);
                            setIsLiability(false);

                            // Set the selected type to true
                            const selected = e.target.value;
                            if (selected === "asset") setIsAsset(true);
                            else if (selected === "expense") setIsExpense(true);
                            else if (selected === "capital") setIsCaptial(true);
                            else if (selected === "liability") setIsLiability(true);
                            }}
                            defaultValue=""
                        >
                            <option value="" disabled>Select Account Type</option>
                            <option value="asset">Asset</option>
                            <option value="expense">Expense</option>
                            <option value="capital">Capital</option>
                            <option value="liability">Liability</option>
                        </select>
                    </div>

                    <div className="Remember forgot password">
                        <label><input type="checkbox" /> Remember me </label>
                        <a href='#'> Forgot Password ?</a>
                    </div>
    
                    <button type="submit" className="btn">Login</button>
    
                    <div className="register-link">
                        <p> Don't have an account? <a href='#' > Register </a></p></div>
    
                </form>
                CreateAccountForm</div>
        );
    
};
export default CreateAccountForm;