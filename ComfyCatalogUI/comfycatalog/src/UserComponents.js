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
import loading from './Utils/Icons/loading.svg';

function UserComponents() {

  const token = getToken();
  const userID = getUserID();
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate('/'); // Navigate to home page if token is not found
    }
    else{console.log(userID);}
  }, [token, navigate]);
  
    return (
          <div>
              {/* <div className='titleContainer'>
                <h1> COMFYSOCKS </h1>
              </div> */}
              <div className='sidebarContainer'>
                <Sidebar /> 
              </div>
              <div >
                <UserProducts/>  
              </div>
          </div>
    );
  }

export default UserComponents;