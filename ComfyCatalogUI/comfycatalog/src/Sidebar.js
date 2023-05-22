import React from 'react';
import { NavLink } from 'react-router-dom';
import './CSS/Sidebar.css';
import football from './Utils/Icons/football.svg';
import heart from './Utils/Icons/heart.svg';
import pencil3 from './Utils/Icons/pencil3.svg';
import tm from './Utils/Icons/tm.svg';
import trademark2 from './Utils/Icons/trademark2.svg';
import logoutIcon from './Utils/Icons/logout.svg';
import Logout from './Logout';

const Sidebar = ({ handleLogout }) => {
  return (
    <div className="sidebar">
      <ul className="ulSidebar">
        <li className="liSidebar">
          <NavLink to="/UserBrands" className="nav-linkSidebar">
            <img src={trademark2} alt="Brands Icon" className="nav-icon" title="Brands" />
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserFavourites" className="nav-linkSidebar">
            <img src={heart} alt="Favorites Icon" className="nav-icon" title="Favorites" />
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserSports" className="nav-linkSidebar">
            <img src={football} alt="Sports Icon" className="nav-icon" title="Sports" />
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserObservations" className="nav-linkSidebar">
            <img src={pencil3} alt="Observations Icon" className="nav-icon" title="Observations" />
          </NavLink>
        </li>
      </ul>
      <div className="liSidebar"style={{ marginTop: '90px' }}>
      <NavLink onClick={handleLogout} to="/Logout" className="nav-linkSidebar">
          <img src={logoutIcon} alt="Logout Icon" className="nav-icon" title="Logout" />
        </NavLink>
      </div>
    </div>
  );
};

export default Sidebar;








