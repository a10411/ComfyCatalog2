import React from 'react';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    
    const token = localStorage.getItem('token');
    console.log(token);
    localStorage.removeItem('token');

    console.log(token);
    // Example: Redirect to the login page after logout
    navigate('/');
  };

  return (
    <button onClick={handleLogout}></button>
  );
};

export default Logout;

