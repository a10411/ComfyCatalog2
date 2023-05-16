import { useState } from 'react';
import './CSS/Login.css';
import { variables } from './Utils/Variables';

function LoginComponent() {
  const API_URL = variables.API_URL;
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loginType, setLoginType] = useState('user'); // default to user login

  const handleLoginTypeChange = (event) => {
    setLoginType(event.target.value);
  };

  const handleLogin = async () => {
    try {
        const response = await fetch(`${API_URL}/api/Login${loginType === 'admin' ? 'Admin' : 'User'}?username=${username}&password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             
            },
        });
        const data = await response.json();
        if (response.ok) {
            // login was successful, store the JWT token and redirect to appropriate page
            localStorage.setItem('token', data.token);
            window.location.href = loginType === 'admin' ? '/AdminComponents' : '/UserComponents';
        } else {
            // login failed, display error message
            setError(data.message);
        }
    } catch (error) {
        console.error(error);
        setError('An error occurred while logging in.');
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
          <span style={{ paddingRight: '10px' }}>Login as:</span>
          <select value={loginType} onChange={handleLoginTypeChange}>
            <option value="user">User</option>
            <option value="admin">Admin</option>
          </select>
        </label>
        <br />
        <button type="button" className='submitLoginRegister' onClick={handleLogin}>Submit</button>
      </form>
    </div>
  );
}

export default LoginComponent;


