import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { FaUser, FaLock } from "react-icons/fa";
import LoginForm from '../Components/LoginForm/LoginForm';
import { LoginDto } from "../Models/Dtos"; // Adjust the import path as necessary
import { baseAPIPath } from "../services/baseServices"; // Adjust the import path as necessary
import { saveUserPreferencesAndToken } from "../services/PersistenceServices";

// Create a type for the credentials object that LoginForm will send up.
// This is good practice as the form's internal state (camelCase) might differ from the API DTO (PascalCase).
interface LoginCredentials {
  username: string;
  password: string;
}

export default function LoginPage() {

  const navigate = useNavigate();

  // 2. Define the handleLogin function. This is your "middleware" logic.
  const handleLogin = async (credentials: LoginCredentials) => {
    // credentials will be an object like { username: 'user123', password: '...' }
    console.log("LoginPage received credentials:", credentials);

    // This is the mapping step. You convert the data from the form's shape
    // to the exact shape the API requires.
    const payload: LoginDto = {
      UserName: credentials.username,
      Password: credentials.password
    };


    // --- THIS IS WHERE YOUR API CALL LOGIC GOES ---
    try {
      console.log("Sending payload to API:", payload);
      const response = await axios.post(baseAPIPath + 'User/login', payload);

      // Save user preferences and auth token using a helper from PersistenceServices
      await saveUserPreferencesAndToken(response.data.result.accessLevel, response.data.result.token);
      // If the API call is successful:
      console.log(response.data);
      alert('Login successful!');

      // Navigate the user to the dashboard page
      navigate('/dashboard');

    }
    catch (error) {
      // This block catches network errors or errors thrown from the try block
      const errorMessage = (error as any).response?.data || (error as Error).message || "An unknown error occurred.";
      console.error('Login failed:', errorMessage);
      alert(`Login failed: ${errorMessage}`);
    }
  };

  // The LoginPage component renders the LoginForm and gives it the handleLogin function
  return <LoginForm onLogin={handleLogin} />;
}