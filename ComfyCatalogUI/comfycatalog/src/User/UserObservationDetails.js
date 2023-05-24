import React from 'react';

function UserObservationDetails({ observation }) {
  return (
    <div className="observation-details-container">
      <div className="observation-details-header">
        <h2 className="observation-title">{observation.title}</h2>
        <p className="observation-date">{observation.date}</p>
      </div>
      <div className="observation-details-body">
        <p>{observation.body}</p>
      </div>
    </div>
  );
}

export default UserObservationDetails;