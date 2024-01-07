import axios from 'axios';

export const fetchProducts = async () => {
  try {
    const response = await axios.get('http://localhost:5026/api/v1/product');
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching products:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};

export const addProductToBasket = async (productId, quantity) => {
  try {
    const response = await axios.post('http://localhost:5026/api/v1/basketdetail', {
      productId,
      quantity,
    });

    return {
      success: true,
      message: 'Product added to basket successfully.',
      data: response.data,
    };
  } catch (error) {
    console.error('Error adding product to basket:', error.message);

    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};


export const getCurrentBasketDetails = async () => {
  try {
    const response = await axios.get('http://localhost:5026/api/v1/basketdetail/list');
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching current basket details:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};