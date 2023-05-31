import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { variables } from '../Utils/Variables';
import Sidebar from '../Sidebar';
import { useNavigate } from 'react-router-dom';
import '../CSS/UserObservationDetails.css';
import { getToken } from '../Global';

function UserObservationDetails() {
  const API_URL = variables.API_URL;
  const [observation, setObservation] = useState(null);
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();
  const location = useLocation();
  const observationID = location.state.observationID;

  useEffect(() => {
    const fetchObservationDetails = async () => {
      try {
        const token = getToken();
        if (!token) {
          setUnauthorized(true);
          setNotification('Unauthorized: Please login to access this page.');
          return;
        }
        const response = await fetch(`${API_URL}/api/GetObservationByID/${observationID}`);
        if (response.ok) {
          const data = await response.json();
          setObservation(data.data);
          console.log(data);
        } else {
          setUnauthorized(true);
          // Handle error cases
          console.error('Failed to fetch observation');
        }
      } catch (error) {
        setUnauthorized(true);
        // Handle network or other errors
        console.error('Error occurred while fetching observation', error);
      }
    };

    fetchObservationDetails();
  }, []);

  return (
    <div>
      {unauthorized ? (
        <div>
          <p>{notification}</p>
          <button className='goToLogin' onClick={() => navigate('/') }>Go to Login</button>
        </div>
      ) : (
        <div className="observation-details-container">
          <Sidebar/>
          <div className="observation-details-header">
            <p className="observation-title">{observation?.title}</p>
            <p className="observation-date">{observation?.date_Hour}</p>
          </div>
          <div className="observation-details-body">
            <p className="observation-text">{observation?.body}</p>
          </div>
        </div>
      )}
    </div>
  );
  

  
}

export default UserObservationDetails;