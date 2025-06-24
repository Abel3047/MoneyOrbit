import React, { useState } from 'react';
import axios from 'axios'; // For making API requests
import { UserCreationDto } from '../../Models/Dtos';

interface CreateUserProps {
    onCreateUser: (credentials: UserCreationDto) => void;
}

const CreateUserForm: React.FC<CreateUserProps> = ({ onCreateUser }) => {

    const [UserName, setUserName] = useState('');
    const [password, setpassword] = useState('');
    const [FirstName, setFirstName] = useState('');
    const [LastName, setLastName] = useState('');
    const [AccessLevel, setAccessLevel] = useState('');
    const [Email, setEmail] = useState('');
    const [PhoneNumber, setPhoneNumber] = useState('');

    const handleSubmit = (e: React.FormEvent) => {
        // 1. Prevent the default form submission (which causes a page refresh)
        e.preventDefault();

        onCreateUser({
            UserName,
            password,
            FirstName,
            LastName,
            AccessLevel,
            Email,
            PhoneNumber
        });
    };

    return (
        <div className="wrapper">
            <form onSubmit={handleSubmit}>
                <h1>Sign Up</h1>

                {/*Username */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="User Name"
                        required value={UserName} onChange={(e) => setUserName(e.target.value)} />
                </div>
                {/*Password */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="Password"
                        required value={password} onChange={(e) => setpassword(e.target.value)} />
                </div>
                {/*First Name  */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="First Name"
                        required value={FirstName} onChange={(e) => setFirstName(e.target.value)} />
                </div>
                {/*Last Name */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="Last Name"
                        required value={LastName} onChange={(e) => setLastName(e.target.value)} />
                </div>
                {/*Access Level */} 
                <div className="input-box">
                    <label htmlFor="accessLevel">Access Level</label>
                    <select
                        id="accessLevel"
                        onChange={(e) => {
                            // Reset access level
                            setAccessLevel("");

                            // Set the selected access level
                            const selected = e.target.value;
                            if (selected === "Developer") setAccessLevel("Developer");
                            else if (selected === "Administrative") setAccessLevel("Administrative");
                            else if (selected === "Customer") setAccessLevel("Customer");
                        }}
                        defaultValue=""
                    >
                        <option value="" disabled>Select Access Level</option>
                        <option value="Developer">Developer</option>
                        <option value="Administrative">Administrator</option>
                        <option value="Customer">Customer</option>
                    </select>
                </div>
                {/*Email */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="Email"
                        required value={Email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                {/*Phone Number */} 
                <div className="input-box">
                    <input type="text"
                        placeholder="Phone Number"
                        required value={PhoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
                </div>

                <button type="submit" className="btn">Sign Up</button>

            </form>
            CreateUserForm</div>
    );

};
export default CreateUserForm;
