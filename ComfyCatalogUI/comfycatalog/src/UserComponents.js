import UserProducts from './User/UserProducts';
import './CSS/App.css';
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