import './CSS/App.css';
import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import LoginComponent from './LoginComponent';
import RegisterComponent from './RegisterComponent';
import MotoLogo from './Utils/MotoLogo';
import BackgroundImage from './Utils/Background';

function HomeComponent() {

  
  return (
    <div className="AppContainer">
        <BackgroundImage />
        <nav className="navbar navbar-expand-sm navbar-light bg-light">
        <MotoLogo />
          <ul className="navbar-nav3 mr-auto">
            <li className='nav-item m-1'>
              <a href="https://www.comfysocks.dk/" target="_blank" className="nav-link">Home</a>
            </li>
          </ul>
          <ul className="navbar-nav2 ml-auto">
            <li className='nav-item m-1'>
              <NavLink className="nav-link" to="/LoginComponent">Login </NavLink>
            </li>
            <li className='nav-item m-1'>
              <NavLink className="nav-link" to="/RegisterComponent" >Register </NavLink>
            </li>
          </ul>
        </nav>
 
        

    </div>
  );
}

export default HomeComponent;
