import { useState } from 'react';
import './CSS/Login.css';
import { variables } from './Utils/Variables';
import { setUserID, setToken } from './Global.js';

function RegisterComponent() {
  const API_URL = variables.API_URL;
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  
  const handleRegister = async () => {
    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    try {
      const response = await fetch(`${API_URL}/api/Register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: username,
          password: password
        })
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
      <form>
        <label>
          Username:
          <input type="text" value={username} onChange={(event) => setUsername(event.target.value)} />
        </label>
        <br />
        <label>
          Password:
          <input type="password" value={password} onChange={(event) => setPassword(event.target.value)} />
        </label>
        <br />
        <label>
          Confirm Password:
          <input type="password" value={confirmPassword} onChange={(event) => setConfirmPassword(event.target.value)} />
        </label>
        <br />
        <button type="button" className="submitLoginRegister" onClick={handleRegister}>
          Register
        </button>
      </form>
    </div>
  );
}

export default RegisterComponent;
