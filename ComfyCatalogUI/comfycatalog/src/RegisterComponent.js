import React, { useState } from 'react';
import './CSS/Register.css';
import { variables } from './Utils/Variables';
import { setUserID, setToken } from './Global.js';

function RegisterComponent() {
  const API_URL = variables.API_URL;
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [passwordStrength, setPasswordStrength] = useState('');

  const handlePasswordChange = (event) => {
    const passwordValue = event.target.value;
    setPassword(passwordValue);
    setPasswordStrength(checkPasswordStrength(passwordValue));
  };

  const checkPasswordStrength = (password) => {
    // Perform your password strength checks here
    // You can implement any logic or use external libraries to determine the password strength
    // For example, you can check the length, presence of uppercase, lowercase, numbers, symbols, etc.
    // Return a string indicating the password strength (e.g., "Weak", "Medium", "Strong")
    if (password.length < 6) {
      return 'Weak';
    } else if (password.length < 8) {
      return 'Medium';
    } else {
      return 'Strong';
    }
  };

  const getPasswordStrengthColor = (passwordStrength) => {
    // Return the color for the password strength bar based on the strength value
    switch (passwordStrength) {
      case 'Weak':
        return 'red';
      case 'Medium':
        return 'yellow';
      case 'Strong':
        return 'green';
      default:
        return '';
    }
  };

  const handleRegister = async () => {
    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      return;
    }

    try {
      const url = `${API_URL}/api/RegisterUser?username=${username}&password=${password}`;
  
      const response = await fetch(url, {
        method: 'POST',
      });
      const data = await response.json();

      if (response.ok) {
        // Registration successful, redirect to LoginComponent
        window.location.href = '/LoginComponent';
      } else {
        // Registration failed, display error message
        setError(data.message);
      }
    } catch (error) {
      console.error(error);
      setError('An error occurred while registering.');
    }
  };

  return (
    <div>
      {error && <div className="error">{error}</div>}
      <div className='form-container'>
      <form className='form-register'>
        <label>
          Username:
          <input type="text" value={username} onChange={(event) => setUsername(event.target.value)} />
        </label>
        <br />
        <label>
          Password:
          <input type="password" value={password} onChange={handlePasswordChange} />
        </label>
        <div className="password-strength-bar">
        <div
        className="password-strength-indicator"
        style={{
          width: `${Math.min((password.length / 10) * 100, 100)}%`,
          background: getPasswordStrengthColor(passwordStrength),
        }}
      ></div>
        </div>
        <span>Password Strength: {passwordStrength}</span>
        <br />
        <label className="confirmPassword">
          Confirm Password:
          <input type="password" value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)} />
        </label>
        <br />
        <button type="button" className="submitLoginRegister" onClick={handleRegister}>
          Register
        </button>
      </form>
      </div>
    </div>
  );
}

export default RegisterComponent;


