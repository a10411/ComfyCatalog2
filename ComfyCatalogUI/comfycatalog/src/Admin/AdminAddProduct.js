import React, { useState, useEffect } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/AddProductForm.css';

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

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setProduct((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`${API_URL}/api/AddProduct`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(product),
      });

      if (response.ok) {
        // Product added successfully
        // Handle success scenario (e.g., show a success message)
      } else {
        // Failed to add product
        // Handle error scenario (e.g., show an error message)
      }
    } catch (error) {
      console.error('An error occurred while adding the product:', error);
    }
  };

  useEffect(() => {
    // Fetch brands and products data from the API
    const fetchData = async () => {
      try {
        const brandsResponse = await fetch(`${API_URL}/api/brands`);
        const brandsData = await brandsResponse.json();
        setBrands(brandsData);

        const productsResponse = await fetch(`${API_URL}/api/products`);
        const productsData = await productsResponse.json();
        setProducts(productsData);
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
              <option value="">Select Brand</option>
              {brands.map((brand) => (
                <option key={brand.brandID} value={brand.brandID}>
                  {brand.brandName}
                </option>
              ))}
            </select>
          </label>
          <label>
            Sport:
            <select
              name="sportID"
              value={product.sportID}
              onChange={handleInputChange}
            >
              <option value="">Select Sport</option>
              {products.map((sport) => (
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
      </div>
    </div>
  );
}

export default AdminAddProduct;

