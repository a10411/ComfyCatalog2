import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import LoginComponent from './LoginComponent';
import UserComponents from './UserComponents';
import AdminComponents from './AdminComponents';
import HomeComponent from './HomeComponent';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomeComponent />} />
        <Route path="/LoginComponent" element={<LoginComponent />} />
        <Route path="/UserComponents" element={<UserComponents />} />
        <Route path="/AdminComponents" element={<AdminComponents />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
