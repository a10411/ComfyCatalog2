import React, { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import { useNavigate } from 'react-router-dom';
import '../CSS/UserProducts.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import SidebarAdmin from '../SidebarAdmin';

function AdminAllFavorites() {
  const API_URL = variables.API_URL;
  const [favorites, setFavorites] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [unauthorized, setUnauthorized] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchFavoritesData = async () => {
      const fetchedFavorites = await fetchFavorites();
      setFavorites(fetchedFavorites);
      fetchImages();
    };

    fetchFavoritesData();
  }, []);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const navigateToProductDetailsAdmin = (productID) => {
    const imageURL = images.find((image) => image.productID === productID)?.imageName;
    navigate('/AdminProductDetail', { state: { productID, imageURL } });
  };

  const fetchFavorites = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetAllFavRelations`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData.data)) {
          const fetchedFavorites = responseData.data;
          return fetchedFavorites;
        } else {
          console.error('Favorites data is not in the expected format:', responseData);
        }
      } else if (response.status === 401) {
        setUnauthorized(true);
      } else {
        console.error('Failed to fetch favorites:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching favorites:', error);
    }

    return [];
  };

  const fetchImages = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetAllImages`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData.data)) {
          setImages(responseData.data);
          setImagesFetched(true);
        } else {
          console.error('Images data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch images:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching images:', error);
    }
  };

  const filteredFavorites = favorites.filter((favorite) => {
    const username = favorite.username || '';
    return username.toLowerCase().includes(searchTerm.toLowerCase());
  });

  if (!Array.isArray(favorites) || favorites.length === 0) {
    return <div>No favorites found.</div>;
  }

  if (!imagesFetched) {
    return (
      <div>
        <img src="loading.svg" alt="Loading" />
      </div>
    );
  }

  return (
    <div>
      <h1>
        <div className="search-input-wrapper">
          <input
            type="text"
            value={searchTerm}
            onChange={handleSearch}
            placeholder="Search by username..."
            className="search-input"
          />
        </div>
      </h1>
      <div className="sidebarContainerAdmin">
        <SidebarAdmin />
      </div>
      {unauthorized ? (
        <div>
          <p>Unauthorized: Please login to access this page.</p>
          <button className="goToLogin" onClick={() => navigate('/')}>
            Go to Login
          </button>
        </div>
      ) : (
        <div className="product-containerFav">
          {filteredFavorites.length === 0 ? (
            <div className="no-favorites">No favorites found.</div>
          ) : (
            filteredFavorites.map((favorite) => (
              <div key={favorite.productID} className="product-card">
                {images
                  .filter((image) => image.productID === favorite.productID)
                  .map((image) => (
                    <img
                      key={image.imageID}
                      src={`${API_URL}/api/GetImage/${image.imageName}`}
                      alt={favorite.productInfo?.productName}
                      className="product-image"
                    />
                  ))}
                <div className="product-name">{favorite.username}</div>
                <div className="product-icons">
                  <FontAwesomeIcon
                    icon={faInfoCircle}
                    className="product-icon"
                    onClick={() => navigateToProductDetailsAdmin(favorite.productID)}
                  />
                </div>
              </div>
            ))
          )}
        </div>
      )}
    </div>
  );
}

export default AdminAllFavorites;
