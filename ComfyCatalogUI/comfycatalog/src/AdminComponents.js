import React from 'react';

import {AdminAddProduct} from './Admin/AdminAddProduct';
import {AdminProductDetail} from './Admin/AdminProductDetail';
import {AdminProducts} from './Admin/AdminProducts';
import {AdminAllObservations} from './Admin/AdminAllObservations';
import {AdminObservationDetail} from './Admin/AdminObservationDetail'
import {AdminBrands} from './Admin/AdminBrands';
import {AdminSports} from './Admin/AdminSports';
import Sidebar from './Sidebar';


function AdminComponents() {
    return (
      <div>
          <div className='sidebarContainer'>
            <Sidebar /> 
          </div>
        {/* add Admin components here */}
      </div>
    );
  }
  
export default AdminComponents;

