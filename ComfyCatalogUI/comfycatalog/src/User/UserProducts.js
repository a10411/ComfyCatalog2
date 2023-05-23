import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/App.css';
import { useNavigate } from 'react-router-dom';


function UserProducts() {
  const API_URL = variables.API_URL;
  const [products, setProducts] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [userID, setUserID] = useState(null);
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();



  useEffect(() => {
    fetchProducts();
    fetchImages();
     // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
    
  }, []);




  const fetchProducts = async () => {
    try {
      const token = localStorage.getItem('token');
  
      if (token) {
        const response = await fetch(`${API_URL}/api/GetAllProducts`, {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        if (response.ok) {
          const responseData = await response.json();
          console.log(responseData);
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
    return <div>Loading images...</div>;
  }

  if (!Array.isArray(images)) {
    return <div>Images are not available.</div>;
  }

  return (
    <div>
      {unauthorized ? (
        <div>
          <p>Unauthorized: Please login to access this page.</p>
          <button className='goToLogin' onClick={() => navigate('/') }>Go to Login</button>
        </div>
      ) : (
        <div className="product-container">
          {products.map((product) => (
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
            </div>
          ))}
        </div>
      )}
    </div>
  );
  
}

export default UserProducts;

