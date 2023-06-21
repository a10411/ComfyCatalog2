import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import './CSS/SidebarAdmin.css';
import threeLines from './Utils/Icons/threeLines.svg';
import football from './Utils/Icons/football.svg';
import heart from './Utils/Icons/heart.svg';
import pencil3 from './Utils/Icons/pencil3.svg';
import trademark2 from './Utils/Icons/trademark2.svg';
import logoutIcon from './Utils/Icons/logout.svg';
import sock from './Utils/Icons/sock.svg';

const SidebarAdmin = ({ handleLogout }) => {

  return (
    <div className= "sidebarAdmin">
      <ul className="ulSidebarAdmin">
        <li className="liSidebarAdmin">
          <NavLink to="/AdminAllFavourites" className="nav-linkSidebarAdmin">
            <div className="nav-link-icon-containerAdmin">
              <img src={heart} alt="Favorites Icon" className="nav-icon" title="All Favorites" />
            </div>
          </NavLink>
        </li>
        <li className="liSidebarAdmin">
          <NavLink to="/AdminAllObservations" className="nav-linkSidebarAdmin">
            <div className="nav-link-icon-containerAdmin">
              <img src={pencil3} alt="Observations Icon" className="nav-icon" title="All Observations" />
            </div>
          </NavLink>
        </li>
        <li className="liSidebarAdmin">
          <NavLink to="/AdminComponents" className="nav-linkSidebarAdmin">
            <div className="nav-link-icon-containerAdmin">
              <img src={sock} alt="Products Icon" className="nav-icon" title="Products" />
            </div>
          </NavLink>
        </li>
      </ul>
      <div className="liSidebarOut">
        <NavLink to="/Logout" className="nav-linkSidebarAdmin">
          <div className="nav-link-icon-containerAdmin">
            <img src={logoutIcon} alt="Logout Icon" className="nav-icon" title="Logout" />
          </div>
        </NavLink>
      </div>
    </div>
  );
};

export default SidebarAdmin;