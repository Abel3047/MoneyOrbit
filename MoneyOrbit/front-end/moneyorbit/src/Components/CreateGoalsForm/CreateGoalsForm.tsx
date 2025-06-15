import React, { useState } from 'react';
import {CreateGoalsDto} from '../../Models/Dtos';

interface CreateGoalsProps {
    onCreateGoal: (credentials: CreateGoalsDto) => void;
}

const CreateGoalsForm: React.FC<CreateGoalsProps> = ({ onCreateGoal }) => {
    
    const [date, setDate] = useState('');
    const [goalName, setGoalName] = useState('');
    const [goalDescription, setGoalDescription] = useState('');
    const [accDebitedID, setAccDebitedID] = useState('');
    const [amount, setAmount] = useState('');

    const handleSubmit = (e: React.FormEvent) => {
            // 1. Prevent the default form submission (which causes a page refresh)
            e.preventDefault();

            // Convert the amount string to a number before creating the DTO.
            // Use parseFloat for decimals. The `|| 0` handles cases where the string is empty or invalid.
            const numericAmount = parseFloat(amount) || 0;

            onCreateGoal({ 
                date,
                goalName,
                goalDescription,
                accDebitedID,
                amount: numericAmount
            });
    };

    return (
                <div className="wrapper">
                    <form onSubmit={handleSubmit}>
                        <h1>Sign Up</h1>
                        <div className="input-box">
                            <input type="text"
                                placeholder="date"
                                required value={date} onChange={(e) => setDate(e.target.value)} />
                        </div>
                        <div className="input-box">
                            <input type="text"
                                placeholder="Goal Name"
                                required value={goalName} onChange={(e) => setGoalName(e.target.value)} />
                        </div>
                        <div className="input-box">
                            <input type="text"
                                placeholder="Goal Description"
                                required value={goalDescription} onChange={(e) => setGoalDescription(e.target.value)} />
                        </div>
                        <div className="input-box">
                            <input type="text"
                                placeholder="Account Debited ID"
                                required value={accDebitedID} onChange={(e) => setAccDebitedID(e.target.value)} />
                        </div>
                        <div className="input-box">
                            <input type="text"
                                placeholder="amount"
                                required value={amount} onChange={(e) => setAmount(e.target.value)} />
                        </div>
        
                        <button type="submit" className="btn">Login</button>
        
                    </form>
                    CreateGoalsForm</div>
            );

};
export default CreateGoalsForm;