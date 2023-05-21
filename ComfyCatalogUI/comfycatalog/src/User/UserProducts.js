import { useEffect, useState } from 'react';
import { variables } from '../Utils/Variables';
import '../CSS/App.css';


function UserProducts() {
  const API_URL = variables.API_URL;
  const [products, setProducts] = useState([]);
  const [images, setImages] = useState([]);
  const [imagesFetched, setImagesFetched] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [observationModalOpen, setObservationModalOpen] = useState(false);
  const [observationTitle, setObservationTitle] = useState('');
  const [observationBody, setObservationBody] = useState('');
  const [userID, setUserID] = useState(null);

  useEffect(() => {
    fetchProducts();
    fetchImages();
     // Retrieve the logged-in user's ID (e.g., from session, local storage, or context)
    
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

  const openObservationModal = (product) => {
    setSelectedProduct(product);
    setObservationModalOpen(true);
  };

  const closeObservationModal = () => {
    setSelectedProduct(null);
    setObservationModalOpen(false);
    setObservationTitle('');
    setObservationBody('');
  };

  const submitObservation = async () => {
    try {
      const response = await fetch(`${API_URL}/api/AddObservation`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          productID: selectedProduct.productID,
          title: observationTitle,
          body: observationBody,
          userID: userID,
        }),
      });
      if (response.ok) {
        // Observation successfully submitted
        console.log('Observation submitted successfully');
      } else {
        console.error('Failed to submit observation:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while submitting observation:', error);
    }
    closeObservationModal();
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
          <button onClick={() => openObservationModal(product)}>Add Observation</button>
        </div>
      ))}

      {observationModalOpen && (
        <div className="observation-modal">
          <div className="observation-modal-content">
            <h2>Add Observation</h2>
            <input
              type="text"
              value={observationTitle}
              onChange={(e) => setObservationTitle(e.target.value)}
              placeholder="Observation Title"
            />
            <textarea
              value={observationBody}
              onChange={(e) => setObservationBody(e.target.value)}
              placeholder="Observation Body"
            ></textarea>
            <div>
              <button onClick={submitObservation}>Submit</button>
              <button onClick={closeObservationModal}>Cancel</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default UserProducts;

