import { useState } from 'react';
import './CSS/Login.css';
import { variables } from './Utils/Variables';
import { setAdminID, setUserID, setToken } from './Global.js';



function LoginComponent() {
  const API_URL = variables.API_URL;
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loginType, setLoginType] = useState(''); // default to user login

  const handleLoginTypeChange = (event) => {
    setLoginType(event.target.value);
  };

  const handleLogin = async () => {
    try {
      const response = await fetch(
        `${API_URL}/api/Login${loginType === 'admin' ? 'Admin' : 'User'}?username=${username}&password=${password}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      const data = await response.json();
  
      if (response.ok) {
        const token = data.data;
  
       
        const IDResponse = await fetch(
          `${API_URL}/api/Get${loginType === 'admin' ? 'Admin' : 'User'}IDFromCredentials?username=${username}`,
          {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${token}`, // Include the token in the request headers
            },
          }
        );
  
        if (loginType === 'admin') {
          const adminIDData = await IDResponse.json();
          const adminID = adminIDData.data; // Access the 'adminID' property from 'adminIDData'
  
          setAdminID(adminID);
          console.log(adminID);
          setToken(token);
        }
        else{
          const userIDData = await IDResponse.json();
          const userID = userIDData.data; // Access the 'userID' property from 'userIDData'
          setUserID(userID);
          console.log(userID);
          setToken(token);
          console.log(token);
        }
  
        window.location.href = loginType === 'admin' ? '/AdminComponents' : '/UserComponents';
      } else {
        // Login failed, display error message
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
      <div className='form-container'>
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
        <button type="button" className="submitLoginRegister" onClick={handleLogin}>
          Submit
        </button>
      </form>
      </div>
    </div>
  );
}

export default LoginComponent;

