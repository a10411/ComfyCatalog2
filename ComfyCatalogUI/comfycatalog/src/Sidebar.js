import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import './CSS/Sidebar.css';
import threeLines from './Utils/Icons/threeLines.svg';

import football from './Utils/Icons/football.svg';
import heart from './Utils/Icons/heart.svg';
import pencil3 from './Utils/Icons/pencil3.svg';
import trademark2 from './Utils/Icons/trademark2.svg';
import logoutIcon from './Utils/Icons/logout.svg';
import sock from './Utils/Icons/sock.svg';

const Sidebar = ({ handleLogout }) => {
  const [expanded, setExpanded] = useState(false);

  const toggleExpand = () => {
    setExpanded(!expanded);
  };

  return (
    <div className={`sidebar ${expanded ? 'expanded' : ''}`}>
      <div className="expand-icon" onClick={toggleExpand}>
        <img src={threeLines} alt="Expand Icon" className="nav-icon" title="Expand" />
      </div>
      <ul className="ulSidebar">
        <li className="liSidebar">
          <NavLink to="/UserFavourites" className="nav-linkSidebar">
            <div className="nav-link-icon-container">
              <img src={heart} alt="Favorites Icon" className="nav-icon" title="Favorites" />
              {expanded && <span className="nav-link-title">Favorites</span>}
            </div>
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserBrands" className="nav-linkSidebar">
            <div className="nav-link-icon-container">
              <img src={trademark2} alt="Brands Icon" className="nav-icon" title="Brands" />
              {expanded && <span className="nav-link-title">Brands</span>}
            </div>
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserSports" className="nav-linkSidebar">
            <div className="nav-link-icon-container">
              <img src={football} alt="Sports Icon" className="nav-icon" title="Sports" />
              {expanded && <span className="nav-link-title">Sports</span>}
            </div>
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserObservations" className="nav-linkSidebar">
            <div className="nav-link-icon-container">
              <img src={pencil3} alt="Observations Icon" className="nav-icon" title="Observations" />
              {expanded && <span className="nav-link-title">Observations</span>}
            </div>
          </NavLink>
        </li>
        <li className="liSidebar">
          <NavLink to="/UserComponents" className="nav-linkSidebar">
            <div className="nav-link-icon-container">
              <img src={sock} alt="Products Icon" className="nav-icon" title="Products" />
              {expanded && <span className="nav-link-title">Products</span>}
            </div>
          </NavLink>
        </li>
      </ul>
      <div className="liSidebarOut">
        <NavLink to="/Logout" className="nav-linkSidebar">
          <div className="nav-link-icon-container">
            <img src={logoutIcon} alt="Logout Icon" className="nav-icon" title="Logout" />
            {expanded && <span className="nav-link-title">Logout</span>}
          </div>
        </NavLink>
      </div>
    </div>
  );
};

export default Sidebar;






