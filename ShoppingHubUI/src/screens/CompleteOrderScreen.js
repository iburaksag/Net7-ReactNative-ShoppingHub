import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, FlatList, Alert, StyleSheet, TouchableOpacity } from 'react-native';
import { getBasketDetails, getCurrentBasketTotalPrice, getProductById, completeOrder, removeBasketDetail } from '../services/CompleteOrderService';
import { useNavigation } from '@react-navigation/native';

const CompleteOrderScreen = () => {
  const [orderAddress, setOrderAddress] = useState('');
  const [basketDetails, setBasketDetails] = useState([]);
  const [orderTotal, setOrderTotal] = useState(0);
  const navigation = useNavigation();


  useEffect(() => {
    const fetchData = async () => {
      const basketDetailsResult = await getBasketDetails();

      if (basketDetailsResult.success) {
        setBasketDetails(basketDetailsResult.data);
        loadProductDetails(basketDetailsResult.data);
        loadOrderTotal(basketDetailsResult.data);
      } else {
        Alert.alert('Error', basketDetailsResult.message);
      }
    };

    fetchData();
  }, []);

  const loadProductDetails = async (details) => {
    const detailsWithProducts = await Promise.all(details.map(async (item) => {
      const productResult = await getProductById(item.productId);
      return {
        ...item,
        productDetails: productResult.success ? productResult.data : null,
      };
    }));

    setBasketDetails(detailsWithProducts);
  };

  const loadOrderTotal = async () => {
    const totalResult = await getCurrentBasketTotalPrice();

    if (totalResult.success) {
      const formattedTotal = totalResult.data.toFixed(2);
      setOrderTotal(formattedTotal);
    } else {
      Alert.alert('Error', totalResult.message);
    }
  };

  const handleCompleteOrder = async () => {
    try {
      const response = await completeOrder({
        orderAddress,
        orderTotal: parseFloat(orderTotal), 
      });

      if (response.success) {
        Alert.alert('Success', 'Order completed successfully');
        navigation.navigate('BasketListScreen');
      } else {
        Alert.alert('Error', 'Products must be more than 0.');
      }
    } catch (error) {
      console.error('Error completing order:', error.message);
      Alert.alert('Error', 'An unexpected error occurred while completing the order.');
    }
  };

const handleRemoveBasketDetail = async (basketDetailId) => {
  try {
    const removeResult = await removeBasketDetail(basketDetailId);

    if (removeResult.success) {
      // Remove the item from the state
      setBasketDetails((prevBasketDetails) =>
        prevBasketDetails.filter((item) => item.id !== basketDetailId)
      );

      setBasketDetails((updatedBasketDetails) => {
        loadOrderTotal();
        loadProductDetails(updatedBasketDetails);

        Alert.alert('Success', 'BasketDetail removed successfully');
      });
    } else {
      console.log('test');
      Alert.alert('Error', removeResult.message);
    }
  } catch (error) {
    console.error('Error removing basket detail:', error.message);
    Alert.alert('Error', 'An unexpected error occurred while removing the basket detail.');
  }
};



  return (
    <View style={styles.container}>
      <TextInput
        style={styles.addressInput}
        value={orderAddress}
        onChangeText={(text) => setOrderAddress(text)}
        placeholder="Enter Order Address"
        multiline
      />
      <Text style={styles.basketItemCount}>{`Basket Items: ${basketDetails ? basketDetails.length : 0}`}</Text>
      <Text style={styles.orderTotal}>{`Order Total: ${orderTotal}₺`}</Text>
      <FlatList
        data={basketDetails}
        keyExtractor={(item) => item.id}
        extraData={basketDetails} 
        renderItem={({ item }) => (
          <View style={styles.basketItemContainer} key={item.id}>
            <Text style={styles.basketProp}>{`Product Name: ${item.productDetails?.productName || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Product Code: ${item.productDetails?.productCode || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Quantity: ${item.quantity}`}</Text>
            <Text style={styles.basketProp}>{`Unit Price: ${item.productDetails?.unitPrice || 'N/A'}₺`}</Text>
            <TouchableOpacity
              style={styles.removeButton}
              onPress={() => handleRemoveBasketDetail(item.id)}
            >
              <Text style={styles.removeButtonText}>Remove</Text>
            </TouchableOpacity>
          </View>
        )}
      />
      <TouchableOpacity style={styles.completeOrderButton} onPress={handleCompleteOrder}>
        <Text style={styles.buttonText}>Complete Order</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1, 
    padding: 20,
    justifyContent: 'space-between',
  },
  addressInput: {
    height: 100,
    borderColor: 'gray',
    borderWidth: 1,
    marginBottom: 10,
    padding: 10,
  },
  basketItemCount: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  orderTotal: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 10,
    color: 'green',
  },
  basketItemContainer: {
    flex: 1,  
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 8,
    padding: 16,
    marginVertical: 8,
  },
  basketProp: {
    marginBottom: 5,
    fontSize: 14,
    fontWeight: 'bold',
    color: '#333333'
  },  
  completeOrderButton: {
    backgroundColor: 'blue',
    padding: 10,
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',  
  },
  buttonText: {
    color: 'white',
    fontWeight: 'bold',
  },
    removeButton: {
    backgroundColor: 'red',
    padding: 5,
    borderRadius: 5,
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 8,
  },
  removeButtonText: {
    color: 'white',
    fontWeight: 'bold',
  },
});

export default CompleteOrderScreen;
