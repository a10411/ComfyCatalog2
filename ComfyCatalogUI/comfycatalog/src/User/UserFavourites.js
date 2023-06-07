import React from 'react';
import Sidebar from '../Sidebar';

const UserFavorites = () => {
  return (
    <div className='favouritesContainer'>
      <div>
      <Sidebar/>
      </div>
      <div>
        <h3>UserFavorites</h3>
      </div>
    </div>
  
  );
};

export default UserFavorites;