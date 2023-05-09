import './CSS/App.css';
import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import LoginComponent  from './LoginComponent';
import ComfyLogo from './Static Images/ComfyLogo.jpg'; 


function App() {
  return (
    <div className="App container">
       
      <BrowserRouter>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <ul className="navbar-nav mr-auto">
            <li>
            <a href="https://www.comfysocks.dk/" target="_blank" className="site_link">Home</a>
            </li>
            <li>
              <NavLink to="/LoginComponent" className="navlink"> Login </NavLink>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route exact path="/" component={App} />
          <Route path="/LoginComponent" component={LoginComponent} />
        </Routes>
      </BrowserRouter>
      <div className='Moto_Logo container'>
          <img src={ComfyLogo} alt="ComfyCatalog" className='logo-image' />
          <h3> The best fit for your socks </h3>
      </div>

      
    </div>
  );
}

export default App;