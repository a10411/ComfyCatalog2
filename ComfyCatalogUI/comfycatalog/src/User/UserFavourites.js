import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import Sidebar from '../Sidebar';
import { getUserID } from '../Global';

function UserFavourites() {
  const API_URL = variables.API_URL;
  const [favorites, setFavorites] = useState([]);
  const [images, setImages] = useState([]);
  const [products, setProducts] = useState([]);

  const [imagesFetched, setImagesFetched] = useState(false);
  const [unauthorized, setUnauthorized] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchFavorites();
    fetchImages();
    // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
  }, []);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const fetchFavorites = async () => {
    try {
      const userID = getUserID();
      const response = await fetch(`${API_URL}/api/GetFavouritesByUser?userID=${userID}`);
      if (response.ok) {
        const responseData = await response.json();

        if (Array.isArray(responseData.data)) {
          setFavorites(responseData.data);
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
  };

  const fetchProductDetails = async (productID) => {
    try {
      const response = await fetch(`${API_URL}/api/GetProduct?productID=${productID}`);
      if (response.ok) {
        const responseData = await response.json();
        return responseData.data.productName; // Return the product name
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
        console.log(responseData);
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
        const productName = await fetchProductDetails(favorite.productID);
        if (productName) {
          updatedFavorites.push({ ...favorite, productName });
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
        <div className="product-container">
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
                <div className="product-name">{favorite.productName}</div>
                <div className="product-icons">
                  <FontAwesomeIcon
                    icon={faInfoCircle}
                    className="product-icon"
                    onClick={() => {
                      // Handle the action when the info icon is clicked
                    }}
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

