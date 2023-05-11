import React from 'react';
import { useLocation } from 'react-router-dom';
import '../CSS/BackgroundImage.css';
import ComfyCatalogBackgroundImage from '../StaticImages/sportsimage2.jpg';

function Background() {
  const location = useLocation();

  return (
    <div
      className="background-image"
      style={{
        backgroundImage: `url(${ComfyCatalogBackgroundImage})`,
        animation: location.pathname === '/' ? 'slide-in 1s forwards' : 'slide-out 1s forwards' 
      }}
    ></div>
  );
}

export default Background;