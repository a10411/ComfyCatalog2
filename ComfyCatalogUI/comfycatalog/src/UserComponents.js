import UserProducts from './User/UserProducts';
import {UserProductDetail} from './User/UserProductDetail';
import {UserObservations} from './User/UserObservations';
import {UserObservationDetail} from './User/UserObservationDetails';
import {UserBrands} from './User/UserBrands';
import {UserSports} from './User/UserSports';
import {UserFavourites} from './User/UserFavourites';
import './CSS/App.css';
import { variables } from './Utils/Variables';
import { BrowserRouter } from 'react-router-dom';
import Sidebar from './Sidebar';
import { useNavigate } from 'react-router-dom';
import React, { useEffect, useState } from 'react';
import {getToken, getUserID} from './Global';

function UserComponents() {

  const token = getToken();
  const userID = getUserID();
  const navigate = useNavigate();


  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('UserID'); // Clear the token from localStorage
    navigate('/');
  };

  useEffect(() => {
    if (!token) {
      navigate('/'); // Navigate to home page if token is not found
    }
  }, [token, navigate]);
  
    return (
          <div>
              {/* <div className='titleContainer'>
                <h1> COMFYSOCKS </h1>
              </div> */}
              <div className='sidebarContainer'>
              <Sidebar handleLogout={handleLogout} />
              </div>
              <div className='userProductsContainer'>
                <UserProducts/>  
              </div>
          </div>
    );
  }

export default UserComponents;