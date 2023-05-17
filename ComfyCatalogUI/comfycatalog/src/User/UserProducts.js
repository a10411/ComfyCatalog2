import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';

function UserProducts() {
  const API_URL = variables.API_URL;
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await fetch(`${API_URL}/api/GetAllProducts`);
      if (response.ok) {
        const responseData = await response.json();
        console.log(responseData);
        if (Array.isArray(responseData.data)) {
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
    // Handle the case when products is not an array (e.g., show an error message or return null)
    return <div>Products are not available.</div>;
  }

  return (
    <div>
      {products.map((product) => {
        console.log(product.ProductID); // Check the ProductID values in the console
        <div key={product.ProductID}>{product.ProductName}</div>;
      })}
    </div>
  );

}

export default UserProducts;
