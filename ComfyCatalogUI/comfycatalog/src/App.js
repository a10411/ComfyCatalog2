import React, { useState, useEffect } from 'react';
import { BrowserRouter, Route, Routes , Navigate } from 'react-router-dom';
import LoginComponent from './LoginComponent';
import RegisterComponent from './RegisterComponent';
import UserComponents from './UserComponents';
import AdminComponents from './AdminComponents';
import HomeComponent from './HomeComponent';
import UserBrandsComponent from './User/UserBrands';
import UserFavouritesComponent from './User/UserFavourites';
import UserSportsComponent from './User/UserSports';
import UserObservationsComponent from './User/UserObservations';
import UserObservationDetailsComponent from './User/UserObservationDetails';
import UserAddObservationComponent from './User/UserAddObservation';
import Logout from './Logout'; 



const PrivateRoute = ({ path, element }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('token'));

  useEffect(() => {
    // Check authentication state when component mounts
    const checkAuthentication = () => {
      const token = localStorage.getItem('token');
      setIsAuthenticated(!!token);
    };

    checkAuthentication();
  }, []);

  if (isAuthenticated) {
    return <Route path={path} element={element} />;
  } else {
    return <Navigate to="/login" replace />;
  }
};


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomeComponent />} />
        <Route path="/LoginComponent" element={<LoginComponent />} />
        <Route path="/RegisterComponent" element={<RegisterComponent />} />
        <Route path="/UserComponents" element={<UserComponents />} />
        <Route path="/AdminComponents" element={<AdminComponents />} />
        <Route path="/UserBrands" element={<UserBrandsComponent />} />
        <Route path="/UserFavourites" element={<UserFavouritesComponent />} />
        <Route path="/UserSports" element={<UserSportsComponent />} />
        <Route path="/UserObservations" element={<UserObservationsComponent />} />
        <Route path="/UserObservationDetails" element={<UserObservationDetailsComponent />} />
        <Route path="/UserAddObservation" element={<UserAddObservationComponent/>} />
        <Route path="/Logout" element={<Logout />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;