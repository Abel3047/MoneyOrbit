import React, { useState } from 'react';
import {CreateGoalsDto} from '../../Models/Dtos';

interface CreateGoalsProps {
    onCreateGoal: (credentials: CreateGoalsDto) => void;
}

const CreateGoalsForm: React.FC<CreateGoalsProps> = ({ onCreateGoal }) => {
    
    const [token, setToken] = useState('');
    const [accIDs, setAccIDs] = useState<string[] | undefined>(undefined);
    const [startDate, setStartDate] = useState<Date | undefined>(undefined);
    const [endDate, setEndDate] = useState<Date | undefined>(undefined);
    const [suspenseTransactions, setSuspenseTransactions] = useState(false);

    const handleSubmit = (e: React.FormEvent) => {
            // 1. Prevent the default form submission (which causes a page refresh)
            e.preventDefault();

            onCreateGoal({ 
                token,
                accIDs,
                startDate,
                endDate,
                suspenseTransactions
        });
    };

    return (
                <div className="wrapper">
                    <form onSubmit={handleSubmit}>
                        <h1>Sign Up</h1>
                        <div className="input-box">
                            <input type="text"
                                placeholder="Token"
                                required value={token} onChange={(e) => setToken(e.target.value)} />
                        </div>
        
                        <button type="submit" className="btn">Login</button>
        
                    </form>
                    CreateGoalsForm</div>
            );

};
export default CreateGoalsForm;