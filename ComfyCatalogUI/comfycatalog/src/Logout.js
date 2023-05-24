import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const navigate = useNavigate();
  const [showConfirmation, setShowConfirmation] = useState(false);

  const handleLogout = () => {
    const token = localStorage.getItem('token');
    localStorage.removeItem('token');
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
    <div>
      {showConfirmation ? (
        <div>
          <p>Are you sure you want to logout?</p>
          <button onClick={handleLogout}>Yes</button>
          <button onClick={handleCancel}>No</button>
        </div>
      ) : (
        <button onClick={handleConfirmation}>Logout</button>
      )}
    </div>
  );
};

export default Logout;

