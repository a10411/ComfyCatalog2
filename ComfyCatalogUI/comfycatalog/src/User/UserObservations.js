import { useState, useEffect } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/App.css';
import '../CSS/ObsTable.css';
import Sidebar from '../Sidebar';

function UserObservations({ productId }) {
  const API_URL = variables.API_URL;
  const [observations, setObservations] = useState([]);


  useEffect(() => {
    fetchObservations();
  }, [productId]);

  const fetchObservations = async () => {
    try {
      const response = await fetch(`${API_URL}/Observation`);
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
    <div className='containerObs'>
      
      <Sidebar/>
      <div className='tableWrapper'>
        {observations.length > 0 ? (
          <table>
            <thead>
              <tr>
                <th>Product ID</th>
                <th>User ID</th>
                <th>Title</th>
                <th>Body</th>
                <th>Date_Hour</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {observations.map((observation) => (
                <tr key={observation.observationID}>
                  <td>{observation.productID}</td>
                  <td>{observation.userID}</td>
                  <td>{observation.title}</td>
                  <td>{observation.body.substring(0, 20)}{observation.body.length > 20 ? "..." : ""}</td>
                  <td>{observation.date_Hour ? observation.date_Hour.substring(0, 10) : ''}</td>
                  <td>
                    <button onClick={() => deleteObservation(observation.observationID)}>Delete</button>
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

export default UserObservations;
