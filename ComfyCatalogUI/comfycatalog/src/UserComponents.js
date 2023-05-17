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

function UserComponents() {
    return (
   
            <div>
                <h1> USER COMPONENTS </h1>
                <UserProducts/>
            </div>

     
    );
  }

export default UserComponents;
