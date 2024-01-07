import axios from 'axios';

const loginService = async (email, password) => {
  try {
    const response = await axios.post('http://localhost:5026/api/v1/auth/login', {
      email,
      password,
    });

    if (response.status === 200) {
      return { success: true, user: response.data };
    } else {
      return { success: false, message: response.data.message || 'An unexpected error occurred' };
    }
  } catch (error) {
    if (error.response && error.response.status === 401) {
      // Unauthorized (incorrect credentials) - Extract error message from the response
      const errorMessage = error.response.data.message || 'Invalid email or password';
      return { success: false, message: errorMessage };
    } else {
      // Handle other network errors or exceptions
      console.error('Error during login:', error.message);
      return { success: false, message: 'An unexpected error occurred' };
    }
  }
};

export default loginService;
