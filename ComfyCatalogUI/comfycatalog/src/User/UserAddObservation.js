import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { variables } from '../Utils/Variables';
import { useLocation } from 'react-router-dom';
import { getUserID } from '../Global';
import '../CSS/UserAddObservation.css';
import Sidebar from '../Sidebar';
import { getToken } from '../Global';

function UserAddObservation() {
  const API_URL = variables.API_URL;
  const location = useLocation();
  const productID = location.state?.productID || null;
  const [title, setTitle] = useState('');
  const [body, setBody] = useState('');
  const [imageURL, setImageURL] = useState('');
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');

  useEffect(() => {
    if (productID) {
      const fetchImageURL = async () => {
        try {
          const response = await fetch(`${API_URL}/api/GetImage/${location.state?.imageURL}`);
          
          if (response.ok) {
            const imageURL = response.url;
            
            setImageURL(imageURL);
          } else {
            console.error('Failed to fetch image:', response.statusText);
          }
        } catch (error) {
          console.error('An error occurred while fetching image:', error);
        }
      };

      fetchImageURL();
    }
    
  }, [productID, location.state?.imageURL]);
  

  const handleObservationSubmit = async () => {
    // Fetch the logged-in user's ID (e.g., from session, local storage, or context)
    const userID = getUserID();
  
    // Create an observation object with the userID, productID, title, and body
    const observation = {
      userID: userID,
      productID: productID,
      title: title,
      body: body,
    };
  
    try {
      const token = getToken();
      if (!token) {
        setUnauthorized(true);
        setNotification('Unauthorized: Please login to access this page.');
        return;
      }
  
      const response = await fetch(`${variables.API_URL}/api/AddObservation`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(observation),
      });
  
      if (response.ok) {
        // Observation added successfully
        console.log('Observation added successfully.');
      } else {
        console.error('Failed to add observation:', response.statusText);
        // Handle error case, show error message, etc.
      }
    } catch (error) {
      console.error('An error occurred while adding observation:', error);
      // Handle error case, show error message, etc.
    }
  };
  
  // ...
  
  return (
    
    <div className="addObs-container">
        <Sidebar />
      <img src={imageURL} alt="Product" className="addObs-product-image" />
      <div className='addObs-subcontainer'>
      <input
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Observation Title"
        className="addObs-input-field"
      />
      <textarea
        value={body}
        onChange={(e) => setBody(e.target.value)}
        placeholder="Observation Body"
        className="addObs-textarea-field"
      ></textarea>
      <div className="addObs-button-container">
        <Link to="/UserObservations" className="addObs-submit-button" onClick={handleObservationSubmit}>
          Submit Observation
        </Link>
      </div>
      </div>

    </div>
  );

}

export default UserAddObservation;




