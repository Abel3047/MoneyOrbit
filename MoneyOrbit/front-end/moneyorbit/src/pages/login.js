import React, { useState } from 'react';
import axios from 'axios';

export default function AuthForm() {
  const [isLogin, setIsLogin] = useState(true);
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Basic form validation
    if (!formData.email || !formData.password) {
      alert('Please fill in all fields.');
      return;
    }

    if (!isLogin && formData.password !== formData.confirmPassword) {
      alert('Passwords do not match.');
      return;
    }

    try {
      const url = isLogin ? '/api/login' : '/api/signup';
      const payload = {
        email: formData.email,
        password: formData.password,
      };

      const response = await axios.post(url, payload);
      console.log(response.data);
      alert(isLogin ? 'Login Successful' : 'Sign Up Successful');
    } catch (err) {
      console.error(err);
      alert('Something went wrong. Please try again.');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-teal-500 to-orange-400">
      <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
        <h2 className="text-2xl font-semibold text-center mb-6">
          {isLogin ? 'Login to MoneyOrbit' : 'Sign Up for MoneyOrbit'}
        </h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <input
            type="email"
            name="email"
            placeholder="Email Address"
            value={formData.email}
            onChange={handleChange}
            className="w-full border border-gray-300 p-3 rounded"
          />

          <input
            type="password"
            name="password"
            placeholder="Password"
            value={formData.password}
            onChange={handleChange}
            className="w-full border border-gray-300 p-3 rounded"
          />

          {!isLogin && (
            <input
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              value={formData.confirmPassword}
              onChange={handleChange}
              className="w-full border border-gray-300 p-3 rounded"
            />
          )}

          <button
            type="submit"
            className="w-full bg-teal-500 text-white p-3 rounded font-semibold hover:bg-teal-600"
          >
            {isLogin ? 'Login' : 'Sign Up'}
          </button>
        </form>

        <div className="text-center mt-4">
          <button
            onClick={() => setIsLogin(!isLogin)}
            className="text-sm text-teal-600 hover:underline"
          >
            {isLogin
              ? "Don't have an account? Sign up"
              : 'Already have an account? Log in'}
          </button>
        </div>
      </div>
    </div>
  );
}