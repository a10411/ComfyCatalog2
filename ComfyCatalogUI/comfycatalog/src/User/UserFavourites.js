import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import { useNavigate } from 'react-router-dom';
import '../CSS/UserProducts.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faInfoCircle, faTrash } from '@fortawesome/free-solid-svg-icons'; // Import the trash icon
import Sidebar from '../Sidebar';
import { getUserID } from '../Global';

function UserFavourites() {
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

  const navigateToProductDetails = (productID) => {
    const imageURL = images.find((image) => image.productID === productID)?.imageName;
    navigate('/UserProductDetail', { state: { productID, imageURL } });
  };

  const deleteFavorite = async (productID) => {
    try {
      // Send a request to delete the favorite from the backend
      const userID = getUserID();
      const response = await fetch(`${API_URL}/api/DeleteFavourite?userID=${userID}&productID=${productID}`, {
        method: 'DELETE',
      });

      if (response.ok) {
        // If the deletion is successful, update the favorites state
        const updatedFavorites = favorites.filter((favorite) => favorite.productID !== productID);
        setFavorites(updatedFavorites);
        fetchFavorites();
      } else {
        console.error('Failed to delete favorite:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while deleting favorite:', error);
    }
  };

  const fetchFavorites = async () => {
    try {
      const userID = getUserID();
      const response = await fetch(`${API_URL}/api/GetFavouritesByUser?userID=${userID}`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData.data)) {
          return responseData.data; // Return the fetched favorites data
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
  
    return []; // Return an empty array if the fetch operation fails
  };

  const fetchProductDetails = async (productID) => {
    try {
      const response = await fetch(`${API_URL}/api/GetProduct?productID=${productID}`);
      if (response.ok) {
        const responseData = await response.json();
        return responseData.data.nomeCliente; // Return the product name
      } else {
        console.error('Failed to fetch product details:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching product details:', error);
    }
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

  useEffect(() => {
    const fetchProductData = async () => {
      const updatedFavorites = [];
      for (const favorite of favorites) {
        const nomeCliente = await fetchProductDetails(favorite.productID);
        if (nomeCliente) {
          updatedFavorites.push({ ...favorite, nomeCliente });
        }
      }
      setFavorites(updatedFavorites);
    };

    if (favorites.length > 0) {
      fetchProductData();
    }
  }, [favorites]);

  if (!Array.isArray(favorites)) {
    return <div>Favorites are not available.</div>;
  }

  if (!imagesFetched) {
    return (
      <div>
        <img src="loading.svg" alt="Loading" />
      </div>
    );
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
            placeholder="Search product..."
            className="search-input"
          />
        </div>
      </h1>
      <div className="sidebarContainer">
        <Sidebar />
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
          {favorites.length === 0 ? (
            <div className="no-favorites">No favorites found.</div>
          ) : (
            favorites.map((favorite) => (
              <div key={favorite.productID} className="product-card">
                {images
                  .filter((image) => image.productID === favorite.productID)
                  .map((image) => (
                    <img
                      key={image.imageID}
                      src={`${API_URL}/api/GetImage/${image.imageName}`}
                      alt={favorite.productName}
                      className="product-image"
                    />
                  ))}
                <div className="product-name">{favorite.nomeCliente}</div>
                <div className="product-icons">
                  <FontAwesomeIcon
                    icon={faInfoCircle}
                    className="product-icon"
                    onClick={() => navigateToProductDetails(favorite.productID)}
                  />
                  <FontAwesomeIcon
                    icon={faTrash}
                    className="product-icon"
                    onClick={() => deleteFavorite(favorite.productID)}
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

export default UserFavourites;


