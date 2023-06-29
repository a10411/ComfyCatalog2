import { useState, useEffect } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/App.css';
import '../CSS/ObsTable.css';
import SidebarAdmin from '../SidebarAdmin';
import { useNavigate } from 'react-router-dom';
import { getToken } from '../Global';

function AdminAllObservations({ productId }) {
  const API_URL = variables.API_URL;
  const [observations, setObservations] = useState([]);
  const [selectedObservation, setSelectedObservation] = useState(null);
  const navigate = useNavigate();
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const [usernames, setUsernames] = useState({});

  const handleViewObservation = (observation) => {
    setSelectedObservation(observation);
    navigate('/AdminObservationDetail', { state: { observationID: observation.observationID } });
  };

  useEffect(() => {
    fetchObservations();
  }, [productId]);

  useEffect(() => {
    fetchUsernames();
  }, [observations]);

  const fetchObservations = async () => {
    try {
      const token = getToken();
      if (!token) {
        setUnauthorized(true);
        setNotification('Unauthorized: Please login to access this page.');
        return;
      }
      const response = await fetch(`${API_URL}/api/GetAllObservations`, {
        method: 'GET',
      });

      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData.data)) {
          setObservations(responseData.data);
        } else {
          console.error('Observations data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch observations:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching observations:', error);
    }
  };

  const fetchUsernames = async () => {
    try {
      const userIds = observations.map((observation) => observation.userID);
      const promises = userIds.map(async (userId) => {
        const response = await fetch(`${API_URL}/User/api/GetUser?userID=${userId}`);
        if (response.ok) {
          const responseData = await response.json();
          if (responseData.data && responseData.data.username) {
            return { userId, username: responseData.data.username };
          } else {
            console.error('Username data is not in the expected format:', responseData);
          }
        } else {
          console.error('Failed to fetch username:', response.statusText);
        }
        return { userId, username: userId }; // Return userID as fallback if username fetching fails
      });

      const resolvedPromises = await Promise.all(promises);
      const usernamesData = resolvedPromises.reduce((acc, { userId, username }) => {
        acc[userId] = username;
        return acc;
      }, {});
      setUsernames(usernamesData);
    } catch (error) {
      console.error('An error occurred while fetching usernames:', error);
    }
  };

  const deleteObservation = async (observationId) => {
    try {
      const response = await fetch(`${API_URL}/api/DeleteObservation/${observationId}`, {
        method: 'DELETE',
      });

      if (response.ok) {
        // Refresh the observations after successfully deleting one
        fetchObservations();
      } else {
        console.error('Failed to delete observation:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while deleting observation:', error);
    }
  };

  return (
    <div className="containerObs">
      <SidebarAdmin />
      <div className="tableWrapper">
        {observations.length > 0 ? (
          <table>
            <thead>
              <tr>
                <th>Username</th>
                <th>Product ID</th>
                <th>Title</th>
                <th>Body</th>
                <th>Date_Hour</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {observations.map((observation) => (
                <tr key={observation.observationID}>
                  <td>{usernames[observation.userID]}</td>
                  <td>{observation.productID}</td>
                  <td>{observation.title}</td>
                  <td>{observation.body.substring(0, 20)}{observation.body.length > 20 ? "..." : ""}</td>
                  <td>{observation.date_Hour ? observation.date_Hour.substring(0, 16).replace('T', ' |') : ''}</td>
                  <td>
                    <button className="buttonObsDel" onClick={() => deleteObservation(observation.observationID)}>Delete</button>
                    <button className="buttonObsView" onClick={() => handleViewObservation(observation)}>View</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : (
          <p>No observations found.</p>
        )}
      </div>
    </div>
  );
}

export default AdminAllObservations;
