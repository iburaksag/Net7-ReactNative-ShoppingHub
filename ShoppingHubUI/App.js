import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import LoginScreen from './src/screens/LoginScreen';
import RegisterScreen from './src/screens/RegisterScreen';
import BasketListScreen from './src/screens/BasketListScreen';
import AddProductScreen from './src/screens/AddProductScreen';
import CompleteOrderScreen from './src/screens/CompleteOrderScreen';
import BasketInfoScreen from './src/screens/BasketInfoScreen';

const Stack = createStackNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="LoginScreen" component={LoginScreen} />
        <Stack.Screen name="RegisterScreen" component={RegisterScreen} />
        <Stack.Screen name="BasketListScreen" component={BasketListScreen} />
        <Stack.Screen name="AddProductScreen" component={AddProductScreen} />
        <Stack.Screen name="CompleteOrderScreen" component={CompleteOrderScreen} />
        <Stack.Screen name="BasketInfoScreen" component={BasketInfoScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default App;
