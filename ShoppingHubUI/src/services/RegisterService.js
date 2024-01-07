import axios from 'axios';

const registerService = async (registerData) => {
  try {
    const response = await axios.post('http://localhost:5026/api/v1/auth/register', registerData);

    if (response.status === 200) {
      return { success: true, user: response.data };
    } else {
      return { success: false, message: response.data.message || 'An unexpected error occurred' };
    }
  } catch (error) {
    if (error.response && error.response.status === 400) {
        
      const errorMessage = error.response.data.errors
        ? error.response.data.errors.join('\n')
        : error.response.data.message || 'Registration failed';
      return { success: false, message: errorMessage };
    } else {
      
      console.error('Error during registration:', error.message);
      return { success: false, message: 'An unexpected error occurred' };
    }
  }
};

export default registerService;
