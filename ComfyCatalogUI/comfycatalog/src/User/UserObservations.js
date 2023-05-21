import { useState, useEffect } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/App.css';

function UserObservations({ productId }) {
  const API_URL = variables.API_URL;
  const [observations, setObservations] = useState([]);
  const [newObservation, setNewObservation] = useState('');

  useEffect(() => {
    fetchObservations();
  }, [productId]);

  const fetchObservations = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetObservationsByProductId/${productId}`);
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

  const addObservation = async () => {
    try {
      const response = await fetch(`${API_URL}/api/AddObservation`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ productId, observation: newObservation }),
      });

      if (response.ok) {
        // Refresh the observations after successfully adding a new one
        fetchObservations();
        setNewObservation('');
      } else {
        console.error('Failed to add observation:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while adding observation:', error);
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
    <div>
      <h2>Observations</h2>
      <div>
        <textarea
          value={newObservation}
          onChange={(e) => setNewObservation(e.target.value)}
          placeholder="Enter your observation"
        />
        <button onClick={addObservation}>Add Observation</button>
      </div>
      <div>
        {observations.map((observation) => (
          <div key={observation.observationId}>
            <p>{observation.observation}</p>
            <button onClick={() => deleteObservation(observation.observationId)}>Delete</button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default UserObservations;
