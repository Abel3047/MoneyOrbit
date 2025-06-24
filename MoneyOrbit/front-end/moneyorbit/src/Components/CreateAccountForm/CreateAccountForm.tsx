import React, { useState } from 'react';
import { AccountFormData } from '../../Models/Data';


interface CreateAccountProps {
    onCreateAccount: (credentials: AccountFormData) => void;
    isBankAccount: boolean;
}

const CreateAccountForm: React.FC<CreateAccountProps> = ({ onCreateAccount, isBankAccount }) => {

    const [accountName, setAccountName] = useState('');
    const [description, setDescription] = useState<string | undefined>(undefined);

    const [isAsset, setIsAsset] = useState(false);
    const [isExpense, setIsExpense] = useState(false);
    const [isCaptial, setIsCaptial] = useState(false);
    const [isLiability, setIsLiability] = useState(false);

    const [BankAccountName, setBankAccountName] = useState<string | undefined>(undefined);
    const [BankAccountNumber, setBankAccountNumber] = useState<string | undefined>(undefined);
    const [BankBranchCode, setBankBranchCode] = useState<string | undefined>(undefined);
    const [BankBranchName, setBankBranchName] = useState<string | undefined>(undefined);
    const [BankSwiftCode, setBankSwiftCode] = useState<string | undefined>(undefined);

    console.log("CreateAccountForm isBankAccount prop:", isBankAccount);
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const credentials: AccountFormData = {
            accountName,
            description,
            // If it's a bank account, it must be an asset.
            // This logic correctly sets the value for the submission.
            isAsset: isBankAccount ? true : isAsset,
            isExpense,
            isCaptial,
            isLiability,
            // The isBankAccount flag is passed directly from the prop
            isBankAccount,
            // Bank details are only relevant if isBankAccount is true
            BankAccountName: isBankAccount ? BankAccountName : undefined,
            BankAccountNumber: isBankAccount ? BankAccountNumber : undefined,
            BankBranchCode: isBankAccount ? BankBranchCode : undefined,
            BankBranchName: isBankAccount ? BankBranchName : undefined,
            BankSwiftCode: isBankAccount ? BankSwiftCode : undefined,
        };

        onCreateAccount(credentials);
    };


    return (
        <div className="wrapper">
            <form onSubmit={handleSubmit}>

                {!isBankAccount ? (
                    <>
                        {/* This section shows ONLY when isBankAccount is FALSE 
                        For onboarding creation of accounts */}
                        <h1>Lets create some default financial accounts.</h1>
                        <div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Account Name"
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
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Account Description"
                                    required
                                    // Connect input value to state
                                    value={description}
                                    // Update state when user types
                                    onChange={(e) => setDescription(e.target.value)}
                                />
                            </div>
                        </div>                        
                        <button type="submit" className="btn">Submit</button>
                    </>
                ) : (
                    <>
                        {/* This section shows ONLY when isBankAccount is TRUE 
                        For linking a Bank account */}
                        <h2>Let's request your bank to link your personal account.</h2>
                        <div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Account Name"
                                    required
                                    // Connect input value to state
                                    value={BankAccountName}
                                    // Update state when user types
                                    onChange={(e) => setBankAccountName(e.target.value)}
                                />
                            </div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Account Number"
                                    required
                                    // Connect input value to state
                                    value={BankAccountNumber}
                                    // Update state when user types
                                    onChange={(e) => setBankAccountNumber(e.target.value)}
                                />
                            </div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Branch Code"
                                    required
                                    // Connect input value to state
                                    value={BankBranchCode}
                                    // Update state when user types
                                    onChange={(e) => setBankBranchCode(e.target.value)}
                                />
                            </div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="Branch Name"
                                    required
                                    // Connect input value to state
                                    value={BankBranchName}
                                    // Update state when user types
                                    onChange={(e) => setBankBranchName(e.target.value)}
                                />
                            </div>
                            <div className="input-box">
                                <input
                                    type="text"
                                    placeholder="SWIFT Code"
                                    required
                                    // Connect input value to state
                                    value={BankSwiftCode}
                                    // Update state when user types
                                    onChange={(e) => setBankSwiftCode(e.target.value)}
                                />
                            </div>
                        </div>
                        <button type="submit" className="btn">Submit</button>
                    </>
                )}
            </form>
        </div>
    );

};
export default CreateAccountForm;