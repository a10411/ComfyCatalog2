import { Route, Routes, useLocation } from 'react-router-dom';
import ComfyLogo from '../StaticImages/ComfysocksLogo.png'; 
import '../CSS/MotoLogo.css';

function MotoLogo() {

  return (
          <div className='Moto_LogoContainer'>
            <img src={ComfyLogo} alt="ComfyCatalog" className='logo-image' />
          </div>
  );
}

export default MotoLogo;
