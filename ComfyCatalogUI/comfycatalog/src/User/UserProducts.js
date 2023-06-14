import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/UserProducts.css'
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { faPlus, faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { getUserID } from '../Global';
import { faHeart } from '@fortawesome/free-solid-svg-icons';



function UserProducts() {
  const API_URL = variables.API_URL;
  const [products, setProducts] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [userID, setUserID] = useState(null);
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');


  useEffect(() => {
    fetchProducts();
    fetchImages();
    // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
  }, []);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const filteredProducts = products.filter((product) =>
    product.productName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const navigateToAddObservation = (productID) => {
    const imageURL = images.find(image => image.productID === productID)?.imageName;
    navigate('/UserAddObservation', { state: { productID, imageURL } });
  };
  
  const navigateToProductDetails = (productID) => {
    const imageURL = images.find(image => image.productID === productID)?.imageName;
    navigate('/UserProductDetail', { state: { productID, imageURL } });
  };
  

  const fetchProducts = async () => {
    try {
      const token = localStorage.getItem('token');
      const userID = localStorage.getItem('userID');
 
      
      if (token) {
        const response = await fetch(`${API_URL}/api/GetAllProducts`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        if (response.ok) {
          const responseData = await response.json();

          if (Array.isArray(responseData.data)) {
            setProducts(responseData.data);
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

  if (!Array.isArray(products)) {
    return <div>Products are not available.</div>;
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
      {unauthorized ? (
        <div>
          <p>{notification}</p>
          <button className="goToLogin" onClick={() => navigate('/')}>
            Go to Login
          </button>
        </div>
      ) : (
        <div className="product-container">
          {filteredProducts.length === 0 ? (
            <div className="no-products">No products found.</div>
          ) : (
            filteredProducts.map((product) => (
              <div key={product.productID} className="product-card">
                {images
                  .filter((image) => image.productID === product.productID)
                  .map((image) => (
                    <img
                      key={image.imageID}
                      src={`${API_URL}/api/GetImage/${image.imageName}`}
                      alt={product.productName}
                      className="product-image"
                    />
                  ))}
                <div className="product-name">{product.productName}</div>
                <div className="product-icons">
                  <FontAwesomeIcon
                    icon={faInfoCircle}
                    className="product-icon"
                    onClick={() => navigateToProductDetails(product.productID)}
                  />
                  <FontAwesomeIcon
                    icon={faPlus}
                    className="product-icon"
                    onClick={(e) => {
                      e.stopPropagation();
                      navigateToAddObservation(product.productID);
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

export default UserProducts;

