import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/UserProducts.css';
import { useNavigate } from 'react-router-dom';
import { faPlus, faInfoCircle } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { getAdminID } from '../Global';
import { faHeart } from '@fortawesome/free-solid-svg-icons';

function AdminProducts() {
  const API_URL = variables.API_URL;
  const [products, setProducts] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [userID, setUserID] = useState(null);
  const [unauthorized, setUnauthorized] = useState(false);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [brands, setBrands] = useState([]);
  const [selectedBrand, setSelectedBrand] = useState('');
  const [sports, setSports] = useState([]);
  const [selectedSport, setSelectedSport] = useState('');

  useEffect(() => {
    fetchProducts();
    fetchImages();
    fetchBrands();
    fetchSports();
    // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
  }, []);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  

  const handleFilterByBrand = (brand) => {
    if (!brand) {
      fetchProducts();
    } else {
      fetchProductsByBrand(brand);
    }
    setSelectedBrand(brand);
  };

  const handleFilterBySport = (sport) => {
    if(!sport){
      fetchProducts();
    }else{
      fetchProductsBySport(sport)
    }
    setSelectedSport(sport);
  }
  

  const filterByProductName = (product, searchTerm) => {
    if (!searchTerm) {
      return true; // No search term provided, include all products
    }

    const productName = product.productName || '';
    return productName.toLowerCase().includes(searchTerm.toLowerCase());
  };

  const filterByBrand = (product, selectedBrand) => {
    if (!selectedBrand) {
      return true; // No brand selected, include all products
    }
    return product.brandID === selectedBrand.brandID;
  };

  const filterBySport = (product, selectedSport) => {
    if(!selectedSport){
      return true;
    }
    return product.sportID === selectedSport.sportID;
  }

  const filteredProducts = products.filter((product) => {
    const productNameMatch = filterByProductName(product, searchTerm);
    const brandMatch = filterByBrand(product, selectedBrand);
    const sportMatch = filterBySport(product, selectedSport);
    return productNameMatch && brandMatch && sportMatch;
  });

  const navigateToProductDetails = (productID) => {
    const imageURL = images.find((image) => image.productID === productID)?.imageName;
    navigate('/AdminProductDetail', { state: { productID, imageURL } });
  };

  const fetchProducts = async () => {
    try {
      const token = localStorage.getItem('token');

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

  const fetchBrands = async () => {
    try {
      const response = await fetch(`${API_URL}/api/getAllBrands`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData.data)) {
          setBrands(responseData.data);
        } else {
          console.error('Brands data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch brands:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching brands:', error);
    }
  };

  const fetchProductsByBrand = async (brandName) => {
    try {
      const response = await fetch(`${API_URL}/GetProductsByBrand?brandName=${brandName}`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData)) {
          setProducts(responseData.data);
        } else {
          console.error('Products data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch products:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching products:', error);
    }
  };

  
  const fetchSports = async () => {
    try {
      const response = await fetch(`${API_URL}/api/getAllSports`);
      if (response.ok) {
        const responseData = await response.json();
        console.log(responseData.data);
        if (Array.isArray(responseData.data)) {
          setSports(responseData.data);
        } else {
          console.error('Brands data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch brands:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching brands:', error);
    }
  };
  
  const fetchProductsBySport = async (sportName) => {
    try {
      const response = await fetch(`${API_URL}/GetProductsBySport?sportName=${sportName}`);
      if (response.ok) {
        const responseData = await response.json();
        if (Array.isArray(responseData)) {
          setProducts(responseData.data);
        } else {
          console.error('Products data is not in the expected format:', responseData);
        }
      } else {
        console.error('Failed to fetch products:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching products:', error);
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
            style={{ fontFamily: "sans-serif",textDecoration: "none" }}
          />
        </div>
      </h1>
      <div className="sidebarBrands">
        <div className="sidebar-title">Brands</div>
        <ul className="brand-list">
          <li
            className={selectedBrand === '' ? 'selected' : ''}
            onClick={() => handleFilterByBrand('')}
          >
            All Brands
          </li>
          {brands.map((brand) => (
            <li
              key={brand.brandID}
              className={selectedBrand && selectedBrand.brandID === brand.brandID ? 'selected' : ''}
              onClick={() => handleFilterByBrand(brand)}
            >
              {brand.brandName.substring(0, 17)}
            </li>
          ))}
        </ul>
      </div>
      <div className="sidebarSports">
        <div className="sidebar-title">Sports</div>
        <ul className="sport-list">
          <li
            className={selectedSport === '' ? 'selected' : ''}
            onClick={() => handleFilterBySport('')}
          >
            All Sports
          </li>
          {sports.map((sport) => (
            <li
              key={sport.sportID}
              className={selectedSport && selectedSport.sportID === sport.sportID ? 'selected' : ''}
              onClick={() => handleFilterBySport(sport)}
            >
              {sport.sportName}
            </li>
          ))}
        </ul>
      </div>
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
                </div>
              </div>
            ))
          )}
        </div>
      )}
    </div>
  );
}

export default AdminProducts;