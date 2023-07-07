import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { variables } from '../Utils/Variables';
import SidebarAdmin from '../SidebarAdmin';
import { getUserID } from '../Global';
import '../CSS/ProductDetail.css';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart } from '@fortawesome/free-solid-svg-icons';

function AdminProductDetail() {
  const API_URL = variables.API_URL;
  const location = useLocation();
  const { productID } = location.state;
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [imageURL, setImageURL] = useState('');
  const [isFavorite, setIsFavorite] = useState(false);
  const [showFavoriteMessage, setShowFavoriteMessage] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [brands, setBrands] = useState([]);
  const [sports, setSports] = useState([]);
  const [selectedBrand, setSelectedBrand] = useState('');
  const [selectedSport, setSelectedSport] = useState('');
  const [selectedFile, setSelectedFile] = useState(null);
  const [notification, setNotification] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchProduct();
    fetchImageURL();
    checkIfProductIsFavorite();
    fetchBrands();
    fetchSports();
  }, []);

  const checkIfProductIsFavorite = async () => {
    try {
      const userID = getUserID();
      const response = await fetch(
        `${API_URL}/api/CheckIfProductIsFavourite?userID=${userID}&productID=${productID}`
      );
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
        const response = await fetch(
          `${API_URL}/api/GetProduct?productID=${productID}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (response.ok) {
          const productData = await response.json();
          const sportName = await fetchSport(productData.data?.sportID);
          const brandName = await fetchBrand(productData.data?.brandID);

          const updatedProductData = {
            ...productData.data,
            sportName,
            brandName,
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
    try {
      const response = await fetch(`${API_URL}/api/GetSport?sportID=${sportID}`);
      if (response.ok) {
        const sportData = await response.json();
        return sportData.data.sportName;
      } else {
        console.error('Failed to fetch sport:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching sport:', error);
    }
    return null;
  };

  const fetchBrands = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetAllBrands`);
      if
(response.ok) {
        const brandData = await response.json();
        setBrands(brandData.data);
      } else {
        console.error('Failed to fetch brands:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching brands:', error);
    }
  };

  const fetchSports = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetAllSports`);
      if (response.ok) {
        const sportData = await response.json();
        setSports(sportData.data);
      } else {
        console.error('Failed to fetch sports:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching sports:', error);
    }
  };

  const handleSave = async () => {
    try {
      const updatedProduct = await updateProduct(product);
      setProduct(updatedProduct);
      setIsEditing(false);
      fetchProduct();
  
      // Navigate to the admin component
      navigate('/AdminComponents'); // Replace '/admin' with the desired route
  
    } catch (error) {
      console.error('Failed to update product:', error);
    }
  };

  const handleFileChange = (e) => {
    setSelectedFile(e.target.files[0]);
  };

  const handleFileUpload = async () => {
    try {
      const formData = new FormData();
      formData.append('file', selectedFile);
  
      const response = await fetch(`${API_URL}/api/addImage?productID=${productID}`, {
        method: 'POST',
        body: formData,
      });
  
      if (response.ok) {
        const imageData = await response.json();
        if (imageData.data) {
          const imageID = imageData.data.imageID;
  
          // Update the product with the new image information
          const updatedProduct = {
            ...product,
            imageID: imageID,
          };
          await updateProduct(updatedProduct);
          setProduct(updatedProduct);
        }
  
        setNotification('Image uploaded successfully!');
      } else {
        console.error('Failed to upload image:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while uploading image:', error);
    }
  };
  
  
  
  

  const updateProduct = async (updatedProduct) => {
    try {
      const token = localStorage.getItem('token');
      if (token) {
        const response = await fetch(`${API_URL}/api/UpdateProduct`, {
          method: 'PATCH',
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(updatedProduct),
        });

        if (response.ok) {
          const updatedProductData = await response.json();
          return updatedProductData;
        } else if (response.status === 401) {
          console.error('Unauthorized: Please login to access this page.');
        } else {
          console.error('Failed to update product:', response.statusText);
        }
      } else {
        console.error('Unauthorized: Please login to access this page.');
      }
    } catch (error) {
      console.error('An error occurred while updating product:', error);
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
        <SidebarAdmin />
      </div>
      <div className="productDetail-container">
        <div className="productDetail-content">
          <table className="product-table">
            <tbody>
              <tr>
                <td className="product-label">Product ID:</td>
                <td className="product-value">{product.productID}</td>
              </tr>
              <tr>
                <td className="product-label">Estado:</td>
                <td className="product-value">
                  {isEditing ? (
                    <select
                      value={product.estadoID}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          estadoID: e.target.value,
                        }))
                      }
                    >
                      <option value="1">Ativo</option>
                      <option value="2">Inativo</option>
                    </select>
                  ) : (
                    <span>{product.estadoID === 1 ? 'Ativo' : 'Inativo'}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Brand:</td>
                <td className="product-value">
                  {isEditing ? (
                    <select
                      value={product.brandID}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          brandID: e.target.value,
                          brandName:
                            brands.find((brand) => brand.brandID === e.target.value)?.brandName || '',
                        }))
                      }
                    >
                      {brands.map((brand) => (
                        <option key={brand.brandID} value={brand.brandID}>
                          {brand.brandName}
                        </option>
                      ))}
                    </select>
                  ) : (
                    <span>{product.brandName ? product.brandName : 'n/a'}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Sport:</td>
                <td className="product-value">
                  {isEditing ? (
                    <select
                      value={product.sportID}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          sportID: e.target.value,
                          sportName:
                            sports.find((sport) => sport.sportID === e.target.value)?.sportName ||
                            '',
                        }))
                      }
                    >
                      {sports.map((sport) => (
                        <option key={sport.sportID} value={sport.sportID}>
                          {sport.sportName}
                        </option>
                      ))}
                    </select>
                  ) : (
                    <span>{product.sportName ? product.sportName : 'n/a'}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Composition:</td>
                <td className="product-value">
                  {isEditing ? (
                    <input
                      type="text"
                      value={product.composition}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          composition: e.target.value,
                        }))
                      }
                    />
                  ) : (
                    <span>{product.composition}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Color:</td>
                <td className="product-value">
                  {isEditing ? (
                    <input
                      type="text"
                      value={product.color}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          color: e.target.value,
                        }))
                      }
                    />
                  ) : (
                    <span>{product.color}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Certification:</td>
                <td className="product-value">
                  {isEditing ? (
                    <input
                      type="text"
                      value={product.certification}
                      onChange={(e) =>
                        setProduct((prevProduct) => ({
                          ...prevProduct,
                          certification: e.target.value,
                        }))
                      }
                    />
                  ) : (
                    <span>{product.certification}</span>
                  )}
                </td>
              </tr>
              <tr>
                <td className="product-label">Image:</td>
                <td className="product-value">
                  {isEditing ? (
                    <div>
                      <input type="file" onChange={handleFileChange} />
                      <button className="product-button upload-button" onClick={handleFileUpload}>
                        Upload
                      </button>
                    </div>
                  ) : (
                    <div>
                      {imageURL && (
                        <img src={`${API_URL}/api/GetImage/${imageURL}`} alt="Product Image" />
                      )}
                    </div>
                  )}
                </td>
              </tr>
            </tbody>
          </table>
          <div className="product-buttons">
            {isEditing ? (
              <>
                <button className="product-button save-button" onClick={handleSave}>
                  Save
                </button>
                <button
                  className="product-button cancel-button"
                  onClick={() => setIsEditing(false)}
                >
                  Cancel
                </button>
              </>
            ) : (
              <button className="product-button update-button" onClick={() => setIsEditing(true)}>
                Edit
              </button>
            )}
          </div>
          {notification && (
            <div>
              <p>{notification}</p>
            </div>
          )}
        </div>
        </div>
    </div>
  );
  
}

export default AdminProductDetail;

