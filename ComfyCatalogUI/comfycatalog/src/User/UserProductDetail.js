import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { variables } from '../Utils/Variables';
import Sidebar from '../Sidebar';
import '../CSS/App.css';
import '../CSS/ProductDetail.css';

function UserProductDetail() {
  const API_URL = variables.API_URL;
  const location = useLocation();
  const { productID } = location.state;
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [imageURL, setImageURL] = useState('');

  useEffect(() => {
    fetchProduct();
    fetchImageURL();
  }, []);

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
          setProduct(productData);
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
      const response = await fetch(`${API_URL}/api/GetImage/${location.state?.imageURL}`);
      if (response.ok) {
        const imageURL = response.url;
        setImageURL(imageURL);
      } else {
        console.error('Failed to fetch image:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching image:', error);
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
      <div className="side">
        <Sidebar />
      </div>
      <div className="product-details">
        <div className="product-details-content">
          <p className="product-detail">
            <span className="product-label">Product Name:</span>
            <span className="product-value">{product.productName}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Brand ID:</span>
            <span className="product-value">{product.brandID}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Estado ID:</span>
            <span className="product-value">{product.estadoID}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Sport:</span>
            <span className="product-value">{product.sport}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Composition:</span>
            <span className="product-value">{product.composition}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Colour:</span>
            <span className="product-value">{product.colour}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Client Number:</span>
            <span className="product-value">{product.clientNumber}</span>
          </p>
          <p className="product-detail">
            <span className="product-label">Product Type:</span>
            <span className="product-value">{product.productType}</span>
          </p>
        </div>
        {imageURL && (
          <div className="product-image-container">
            <img className="product-image" src={`${API_URL}/api/GetImage/${imageURL}`} alt="Product" />
          </div>
        )}
      </div>
    </div>
  );
}

export default UserProductDetail;


