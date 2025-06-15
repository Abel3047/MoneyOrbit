import { FaUser, FaLock } from "react-icons/fa";
import React, { useState } from "react"; // <-- Import useState
import "./LoginForm.css";

interface LoginCredentials {
    username: string;
    password: string;
}
// This clearly states that LoginForm expects one prop, `onLogin`, which must be a
// function that accepts an object matching the LoginCredentials shape.
interface LoginFormProps {
    onLogin: (credentials: LoginCredentials) => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onLogin }) => {
    // Create state variables to hold the form data
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    // This function will be called when the form is submitted
    const handleSubmit = (e: React.FormEvent) => {
        // 1. Prevent the default form submission (which causes a page refresh)
        e.preventDefault();

        // When onLogin is called, TypeScript knows the object MUST have
        // a `username` and `password` property, both as strings.
        onLogin({ username, password });
    };

    return (
        <div className="wrapper">
            <form onSubmit={handleSubmit}>
                <h1>Sign Up</h1>
                <div className="input-box">
                    <input type="text"
                        placeholder="Username"
                        required value={username} onChange={(e) => setUsername(e.target.value)} />
                    <FaUser className="icon" />
                </div>
                <div className="input-box">
                    <input
                        // Changed to "password" type for security (hides characters)
                        type="password"
                        placeholder="Password"
                        required
                        // Connect input value to state
                        value={password}
                        // Update state when user types
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <FaLock className="icon" />
                </div>

                <div className="Remember forgot password">
                    <label><input type="checkbox" /> Remember me </label>
                    <a href='#'> Forgot Password ?</a>
                </div>

                <button type="submit" className="btn">Login</button>

                <div className="register-link">
                    <p> Don't have an account? <a href='#' > Register </a></p></div>

            </form>
            LoginForm</div>
    );
};
export default LoginForm;