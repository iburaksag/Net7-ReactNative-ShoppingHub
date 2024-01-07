import axios from 'axios';

const getBasketsByUserId = async () => {
  try {
    const response = await axios.get('http://localhost:5026/api/v1/basket/list');
    return { success: true, baskets: response.data };
  } catch (error) {
    console.error('Error fetching baskets:', error.message);
    return { success: false, message: 'An error occurred while fetching baskets' };
  }
};

const createBasket = async () => {
  try {
    const response = await axios.post('http://localhost:5026/api/v1/basket');
    return { success: true, basketId: response.data.basketId };
  } catch (error) {
    console.error('Error creating a new basket:', error.message);
    return { success: false, message: 'An error occurred while creating a new basket' };
  }
};

export default { getBasketsByUserId, createBasket };
