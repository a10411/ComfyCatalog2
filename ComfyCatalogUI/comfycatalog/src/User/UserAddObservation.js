import { useState } from 'react';
import { variables } from '../Utils/Variables';
import { useLocation } from 'react-router-dom';
import { getUserID } from '../Global';
import '../CSS/UserAddObservation.css';

function UserAddObservation() {
  const location = useLocation();
  const productID = location.state?.productID || null;
  const [title, setTitle] = useState('');
  const [body, setBody] = useState('');

  if (!productID) {
    return <div>No product ID found.</div>;
  }



  const handleObservationSubmit = async () => {
    // Fetch the logged-in user's ID (e.g., from session, local storage, or context)
    const userID = getUserID();

    console.log(userID);
    console.log(productID);

    // Create an observation object with the userID, productID, title, and body
    const observation = {
      userID: userID,
      productID: productID,
      title: title,
      body: body,
    };

    try {
      const response = await fetch(`${variables.API_URL}/api/AddObservation`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(observation),
      });

      if (response.ok) {
        // Observation added successfully
        console.log('Observation added successfully.');
        // Redirect or perform any other action as needed
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
    <div className="container">
      <input
        
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Observation Title"
        className="input-field"
      />
      <textarea
        value={body}
        onChange={(e) => setBody(e.target.value)}
        placeholder="Observation Body"
        className="textarea-field"
      ></textarea>
      <div className="button-container">
        <button onClick={handleObservationSubmit} className="submit-button">
          Submit Observation
        </button>
      </div>
    </div>
  );
}

export default UserAddObservation;


