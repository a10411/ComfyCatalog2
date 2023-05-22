import UserProducts from './User/UserProducts';
import {UserProductDetail} from './User/UserProductDetail';
import {UserObservations} from './User/UserObservations';
import {UserObservationDetail} from './User/UserObservationDetails';
import {UserBrands} from './User/UserBrands';
import {UserSports} from './User/UserSports';
import {UserFavourites} from './User/UserFavourites';
import './CSS/App.css';
import { variables } from './Utils/Variables';
import { BrowserRouter } from 'react-router-dom';
import Sidebar from './Sidebar';
import { useNavigate } from 'react-router-dom';

function UserComponents() {

  const token = localStorage.getItem('token');
  console.log(token);
  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem('token'); // Clear the token from localStorage
    navigate('/');
  };

  
    return (
          <div>
              <div className='titleContainer'>
                <h1> USER COMPONENTS </h1>
              </div>
              <div className='sidebarContainer'>
              <Sidebar handleLogout={handleLogout} />
              </div>
              <div className='userProductsContainer'>
                <UserProducts/>
                
              </div>
          </div>
    );
  }

export default UserComponents;
