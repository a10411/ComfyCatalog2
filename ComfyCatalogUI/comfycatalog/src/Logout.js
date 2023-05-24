import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const navigate = useNavigate();
  const [showConfirmation, setShowConfirmation] = useState(false);

  const handleLogout = () => {
    
    localStorage.removeItem('token');
    
    localStorage.removeItem('userID');
    navigate('/');
  };

  const handleConfirmation = () => {
    setShowConfirmation(true);
  };

  const handleCancel = () => {
    setShowConfirmation(false);
    navigate('/UserComponents'); // Replace 'UserProducts' with the appropriate route
  };

  return (
    <div className="LogoutContainer">
      {showConfirmation ? (
        <div className="LogoutConfirmationContainer">
          <p>Are you sure you want to logout?</p>
          <div className="LogoutButtons">
            <button className="LogoutButton" onClick={handleLogout}>Yes</button>
            <button className="LogoutButton" onClick={handleCancel}>No</button>
          </div>
        </div>
      ) : (
        <button className="LogoutButton" onClick={handleConfirmation}>Logout</button>
      )}
    </div>
  );
};



export default Logout;


