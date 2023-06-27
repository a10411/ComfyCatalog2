import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { variables } from '../Utils/Variables';
import Sidebar from '../Sidebar';
import { getUserID } from '../Global';
import '../CSS/ProductDetail.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart } from '@fortawesome/free-solid-svg-icons';

function UserProductDetail() {
  const API_URL = variables.API_URL;
  const location = useLocation();
  const { productID } = location.state;
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [imageURL, setImageURL] = useState('');
  const [isFavorite, setIsFavorite] = useState(false);
  const [showFavoriteMessage, setShowFavoriteMessage] = useState(false);

  useEffect(() => {
    fetchProduct();
    fetchImageURL();
    checkIfProductIsFavorite();
    fetchBrand();
    fetchSport();
  }, []);

  const checkIfProductIsFavorite = async () => {
    try {
      const userID = getUserID();
      const response = await fetch(`${API_URL}/api/CheckIfProductIsFavourite?userID=${userID}&productID=${productID}`);
      if (response.ok) {
        const isFavorite = await response.json();
        
        
        setIsFavorite(isFavorite.data);
        if (isFavorite) {
          setShowFavoriteMessage(true);
        }
      } else if (response.status === 401) {
        console.error('Unauthorized: Please login to access this page.');
      } else {
        console.error('Failed to fetch favorite status:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching favorite status:', error);
    }
    
  };
  
  

  const fetchProduct = async () => {
    try {
      const token = localStorage.getItem('token');
      if (token) {
        const response = await fetch(`${API_URL}/api/GetProduct?productID=${productID}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
  
        if (response.ok) {
          const productData = await response.json();
          const sportName = await fetchSport(productData.data?.sportID);
          const brandName = await fetchBrand(productData.data?.brandID);
  
          const updatedProductData = {
            ...productData.data,
            sportName,
            brandName
          };
  
          setProduct(updatedProductData);
          setLoading(false);
        } else if (response.status === 401) {
          console.error('Unauthorized: Please login to access this page.');
        } else {
          console.error('Failed to fetch product:', response.statusText);
        }
      } else {
        console.error('Unauthorized: Please login to access this page.');
      }
    } catch (error) {
      console.error('An error occurred while fetching product:', error);
    }
  };
  
  

  const fetchImageURL = async () => {
    try {
      const imageName = location.state?.imageURL;
      if (imageName) {
        const response = await fetch(`${variables.API_URL}/api/GetImage/${imageName}`);
        if (response.ok) {
          setImageURL(imageName);
        } else {
          console.error('Failed to fetch image:', response.statusText);
        }
      }
    } catch (error) {
      console.error('An error occurred while fetching image:', error);
    }
  };

  const fetchBrand = async (brandID) => {
    try {
      const response = await fetch(`${API_URL}/api/GetBrand?brandID=${brandID}`);
      if (response.ok) {
        const brandData = await response.json();
        return brandData.data.brandName;
      } else {
        console.error('Failed to fetch brand:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching brand:', error);
    }
    return null;
  };

  const fetchSport = async (sportID) => {
    try{
      const response = await fetch(`${API_URL}/api/GetSport?sportID=${sportID}`);
      if(response.ok){
        const sportData = await response.json();
        return sportData.data.sportName;
        } else {
          console.error('Failed to fetch brand:', response.statusText);
        }
    } catch (error) {
      console.error('An error occurred while fetching sport:', error);
    }
    return null;
  };

const handleFavoriteClick = async () => {
  try {
    const userID = getUserID();
    const token = localStorage.getItem('token');
    if (token) {
      if (isFavorite) {
        setShowFavoriteMessage(true); 
        console.log('This product is already a favorite.');
      } else {
        const response = await fetch(
          `${API_URL}/api/SetFavoriteProductToUser?userID=${userID}&productID=${productID}`,
          {
            method: 'POST',
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        if (response.ok) {
          setIsFavorite(!isFavorite);
        } else if (response.status === 401) {
          console.error('Unauthorized: Please login to access this page.');
        } else {
          console.error('Failed to set favorite product:', response.statusText);
        }
      }
    } else {
      console.error('Unauthorized: Please login to access this page.');
    }
  } catch (error) {
    console.error('An error occurred while setting favorite product:', error);
  }
};


  if (loading) {
    return <div>Loading product details...</div>;
  }

  if (!product) {
    return <div>Product not found.</div>;
  }

  return (
    <div>
      <div>
        <Sidebar />
      </div>
      <div className="productDetail-container">
        <FontAwesomeIcon
          icon={faHeart}
          className={`favorite-icon ${isFavorite ? 'favorite' : ''}`}
          onClick={handleFavoriteClick}
        />
  
        <div className="productDetail-content">
          <table className="product-table">
            <tbody>
              <tr>
                <td className="product-label">Brand:</td>
                <td className="product-value">
                  {product.brandName ? product.brandName : 'n/a'}
                </td>
              </tr>
              <tr>
                <td className="product-label">Sport:</td>
                <td className="product-value">
                  {product.sportName ? product.sportName : 'n/a'}
                </td>
              </tr>
              <tr>
                <td className="product-label">Composition:</td>
                <td className="product-value">{product.composition}</td>
              </tr>
              <tr>
                <td className="product-label">Color:</td>
                <td className="product-value">{product.color}</td>
              </tr>
              <tr>
                <td className="product-label">Certification:</td>
                <td className="product-value">{product.certification}</td>
              </tr>
              <tr>
                <td className="product-label">Knitting Type:</td>
                <td className="product-value">{product.knittingType}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div className="productDetail-image-container">
          {imageURL && <img className="productDetail-image" src={`${API_URL}/api/GetImage/${imageURL}`} />}
        </div>
      </div>
    </div>
  );
  
}

export default UserProductDetail;

