import { useEffect, useState } from 'react';

function UserProducts() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await fetch('/api/products'); // Adjust the API endpoint URL if needed
      if (response.ok) {
        const data = await response.json();
        setProducts(data);
      } else {
        console.error('Failed to fetch products:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching products:', error);
    }
  };

  return (
    <div>
      <h2>User Products</h2>
      {products.map((product) => (
        <div key={product.id}>
          <h3>{product.name}</h3>
          <img src={product.image} alt={product.name} />
        </div>
      ))}
    </div>
  );
}

export default UserProducts;