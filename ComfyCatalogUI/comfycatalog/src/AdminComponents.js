import AdminProducts from './Admin/AdminProducts';
import SidebarAdmin from './SidebarAdmin';
import { useNavigate } from 'react-router-dom';
import React, { useEffect, useState } from 'react';
import {getToken, getAdminID} from './Global';
import loading from './Utils/Icons/loading.svg';

function AdminComponents() {

  const token = getToken();
  const adminID = getAdminID();
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate('/'); // Navigate to home page if token is not found
    }
    else{console.log(adminID);}
  }, [token, navigate]);



    return (
      <div>
          <div className='sidebarContainerAdmin'>
              <SidebarAdmin /> 
          </div>
          <div >
              <AdminProducts/>  
          </div>
      </div>
    );
  }
  
export default AdminComponents;

