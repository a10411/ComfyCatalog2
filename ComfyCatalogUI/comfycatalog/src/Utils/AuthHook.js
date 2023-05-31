import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getToken } from '../utils/auth'; // Path to the auth utility function

export const useAuthentication = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const token = getToken(); // Retrieve the authentication token

    if (!token) {
      // Token not found, user is not authenticated
      navigate('/'); // Redirect to the login page
    }
  }, [navigate]);
};
