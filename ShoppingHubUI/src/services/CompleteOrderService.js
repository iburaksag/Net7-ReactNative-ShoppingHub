import axios from 'axios';

export const getBasketDetails = async () => {
  try {
    const response = await axios.get('http://localhost:5026/api/v1/basketDetail/list');
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching basket details:', error.message);
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


export const getCurrentBasketTotalPrice = async () => {
  try {
    const response = await axios.get('http://localhost:5026/api/v1/basket/orderTotal');
    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error fetching current basket total price:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred.',
    };
  }
};


export const completeOrder = async ({ orderAddress, orderTotal }) => {
  try {
    const response = await axios.put('http://localhost:5026/api/v1/basket/complete', {
      orderAddress,
      orderTotal,
    });

    return {
      success: true,
      data: response.data,
    };
  } catch (error) {
    console.error('Error completing order:', error.message);
    return {
      success: false,
      message: error.response?.data?.message || 'An unexpected error occurred while completing the order.',
    };
  }
};

export const removeBasketDetail = async (basketDetailId) => {
  try {
    const response = await axios.delete(`http://localhost:5026/api/v1/basketdetail/${basketDetailId}`);

    if (response.data.success) {
      return {
        success: true,
        message: response.data.message,
      };
    } else {
      return {
        success: false,
        message: response.data.message || 'Failed to remove BasketDetail',
      };
    }
  } catch (error) {
    console.error('Error removing basket detail:', error.message);
    return {
      success: false,
      message: 'An unexpected error occurred while removing the basket detail.',
    };
  }
};
