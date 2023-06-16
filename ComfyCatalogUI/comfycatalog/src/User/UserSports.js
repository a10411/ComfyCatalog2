import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/UserBrands.css';
import { useNavigate } from 'react-router-dom';
import Sidebar from '../Sidebar';


function UserSports() {
  const API_URL = variables.API_URL;
  const [sports, setSports] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [userID, setUserID] = useState(null);
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');



  useEffect(() => {
    fetchSports();
 
     // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
    
  }, []);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredBrands = sports.filter((sport) =>
  sport.sportName.toLowerCase().includes(searchTerm.toLowerCase())
);



  const fetchSports = async () => {
    try {
      const token = localStorage.getItem('token');
      const userID = localStorage.getItem('userID');
      console.log(userID)
      if (token) {
        const response = await fetch(`${API_URL}/api/GetAllSports`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        if (response.ok) {
          const responseData = await response.json();
          
          if (Array.isArray(responseData.data)) {
            setSports(responseData.data);
          } else {
            console.error('Products data is not in the expected format:', responseData);
          }
        } else if (response.status === 401) {
          setUnauthorized(true);
          setNotification('Unauthorized: Please login to access this page.');
        } else {
          console.error('Failed to fetch products:', response.statusText);
        }
      } else {
        setUnauthorized(true);
        setNotification('Unauthorized: Please login to access this page.');
      }
    } catch (error) {
      console.error('An error occurred while fetching products:', error);
    }
  };
  
  




  if (!Array.isArray(sports)) {
    return <div>Sports are not available.</div>;
  }



  if (!Array.isArray(images)) {
    return <div>Images are not available.</div>;
  }

  return (
    <div>
      <h1>
      <div className="search-input-wrapper">
      <input 
        type="text"
        value={searchTerm}
        onChange={handleSearch}
        placeholder="Search brand..."
        className="search-input"
      />
      </div>
      </h1>
      <Sidebar />
      {unauthorized ? (
        <div>
          <p>{notification}</p>
          <button className="goToLogin" onClick={() => navigate("/")}>
            Go to Login
          </button>
        </div>
      ) : (
       
          <div className="brand-container">
            {sports
              .filter((sport) =>
                sport.sportName.toLowerCase().includes(searchTerm.toLowerCase())
              )
              .map((sport) => (
                <div key={sport.sportID} className="brand-card">
                  <div className="brand-name">{sport.sportName}</div>
                </div>
                
              ))}
          </div>
   
      )}
    </div>
  );
  
}

export default UserSports;