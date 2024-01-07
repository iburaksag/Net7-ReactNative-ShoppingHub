import axios from 'axios';

export const getBasketDetailsByBasketId = async (basketId) => {
  try {
    const response = await axios.get(`http://localhost:5026/api/v1/basketDetail/${basketId}`);
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching basket details by basketId:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};

export const getProductById = async (id) => {
  try {
    const response = await axios.get(`http://localhost:5026/api/v1/product/${id}`);
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching product by Id:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};