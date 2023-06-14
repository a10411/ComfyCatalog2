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

  useEffect(() => {
    fetchProduct();
    fetchImageURL();
    checkIfProductIsFavorite();
    fetchBrand();
    checkIfProductIsFavorite();
  }, []);

  const checkIfProductIsFavorite = async () => {
    try {
      const userID = getUserID();
      const response = await fetch(`${API_URL}/api/IsProductFavorite?userID=${userID}&productID=${productID}`);
      if (response.ok) {
        const isFavorite = await response.json();
        setIsFavorite(isFavorite); // Update the isFavorite state directly
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
          const brandName = await fetchBrand(productData.data?.brandID);
          setProduct({ ...productData.data, brandName });
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


  

  const handleFavoriteClick = async () => {
    try {
      const userID = getUserID();
      const token = localStorage.getItem('token');
      if (token) {
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
          if (isFavorite) {
            <span className="favorite-message">This product is already a favorite.</span>
            console.log('This product is already a favorite.');
          } else {
            // Success: Update the user's favorite products
            setIsFavorite(!isFavorite);
          }
        } else if (response.status === 401) {
          console.error('Unauthorized: Please login to access this page.');
        } else {
          console.error('Failed to set favorite product:', response.statusText);
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
                <td className="product-label">Product Name:</td>
                <td className="product-value">{product.productName}</td>
              </tr>
              <tr>
                <td className="product-label">Brand Name:</td>
                <td className="product-value">{product.brandName}</td>
              </tr>
              <tr>
                <td className="product-label">Sport:</td>
                <td className="product-value">{product.sport}</td>
              </tr>
              <tr>
                <td className="product-label">Composition:</td>
                <td className="product-value">{product.composition}</td>
              </tr>
              <tr>
                <td className="product-label">Colour:</td>
                <td className="product-value">{product.colour}</td>
              </tr>
              <tr>
                <td className="product-label">Client Number:</td>
                <td className="product-value">{product.clientNumber}</td>
              </tr>
              <tr>
                <td className="product-label">Product Type:</td>
                <td className="product-value">{product.productType}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div className="productDetail-image-container">
          {imageURL && (
            <img className="productDetail-image" src={`${API_URL}/api/GetImage/${imageURL}`} />
          )}
        </div>
      </div>
    </div>
  );
}

export default UserProductDetail;

