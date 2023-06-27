import React, { useState, useEffect } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/AddProductForm.css';
import { useNavigate } from 'react-router-dom';

function AdminAddProduct() {
  const API_URL = variables.API_URL;
  const [product, setProduct] = useState({
    brandID: '',
    estadoID: '',
    sportID: '',
    nomeCliente: '',
    composition: '',
    color: '',
    size: '',
    certification: '',
    knittingType: '',
  });
  const [brands, setBrands] = useState([]);
  const [sports, setSports] = useState([]);
  const [products, setProducts] = useState([]);
  const [notification, setNotification] = useState(null); // State variable for notification message

  const navigate = useNavigate(); // Initialize the useNavigate hook

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    console.log(`Input changed - Name: ${name}, Value: ${value}`);

    // Convert the value to a number for estadoID field
    const processedValue = name === 'estadoID' ? parseInt(value, 10) : value;

    setProduct((prevState) => ({
      ...prevState,
      [name]: processedValue,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Create a product object with the necessary properties
    const productToAdd = {
      brandID: product.brandID,
      estadoID: product.estadoID,
      sportID: product.sportID,
      nomeCliente: product.nomeCliente,
      composition: product.composition,
      color: product.color,
      size: product.size,
      certification: product.certification,
      knittingType: product.knittingType,
    };

    console.log(productToAdd);

    try {
      const response = await fetch(`${API_URL}/api/AddProduct`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(productToAdd), // Pass the product object in the request body as JSON
      });

      if (response.ok) {
        // Product added successfully
        setNotification('Product added successfully'); // Set the notification message
        navigate('/AdminComponents'); // Navigate to the AdminProducts page
      } else {
        // Failed to add product
        const errorData = await response.json();
        console.error('Failed to add product:', errorData);
        // Display the error message to the user
        if (errorData.errors) {
          Object.values(errorData.errors).forEach((error) => {
            console.error(error);
          });
        }
      }
    } catch (error) {
      console.error('An error occurred while adding the product:', error);
    }
  };

  useEffect(() => {
    // Fetch brands and sports data from the API
    const fetchData = async () => {
      try {
        const brandsResponse = await fetch(`${API_URL}/api/GetAllBrands`);
        const brandsData = await brandsResponse.json();
        setBrands(brandsData.data); // Assuming the API response has a 'brands' property that contains the array

        const sportsResponse = await fetch(`${API_URL}/api/GetAllSports`);
        const sportsData = await sportsResponse.json();
        setSports(sportsData.data); // Assuming the API response is an array

        // Set initial values for brandID and sportID in the product state
        setProduct((prevState) => ({
          ...prevState,
          brandID: brandsData.data[0].brandID, // Set the first brand ID as the initial value
          sportID: sportsData.data[0].sportID, // Set the first sport ID as the initial value
        }));
      } catch (error) {
        console.error('An error occurred while fetching data:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className='mainContainerAddProduct'>
      <div className="form-containerAddProduct">
        <form className='addform' onSubmit={handleSubmit}>
          <label>
            Brand:
            <select
              name="brandID"
              value={product.brandID}
              onChange={handleInputChange}
            >
              {brands.map((brand) => (
                <option key={brand.brandID} value={brand.brandID}>
                  {brand.brandName}
                </option>
              ))}
            </select>
          </label>
          <label>
            Estado ID:
            <select
              name="estadoID"
              value={product.estadoID}
              onChange={handleInputChange}
            >
              <option disabled value="">
                Select estado
              </option>
              <option value="1">1</option>
              <option value="2">2</option>
            </select>
          </label>
          <label>
            Sport:
            <select
              name="sportID"
              value={product.sportID}
              onChange={handleInputChange}
            >
              {sports.map((sport) => (
                <option key={sport.sportID} value={sport.sportID}>
                  {sport.sportName}
                </option>
              ))}
            </select>
          </label>
          <label>
            Nome Cliente:
            <input
              type="text"
              name="nomeCliente"
              value={product.nomeCliente}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Composition:
            <input
              type="text"
              name="composition"
              value={product.composition}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Color:
            <input
              type="text"
              name="color"
              value={product.color}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Size:
            <input
              type="text"
              name="size"
              value={product.size}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Certification:
            <input
              type="text"
              name="certification"
              value={product.certification}
              onChange={handleInputChange}
            />
          </label>
          <label>
            Knitting Type:
            <input
              type="text"
              name="knittingType"
              value={product.knittingType}
              onChange={handleInputChange}
            />
          </label>
          <button type="submit">Add Product</button>
        </form>
        {notification && <p>{notification}</p>} {/* Display the notification if it is not null */}
      </div>
    </div>
  );
}

export default AdminAddProduct;
