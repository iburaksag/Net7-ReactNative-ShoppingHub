import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, Button, TouchableOpacity, FlatList, Alert, StyleSheet } from 'react-native';
import { useNavigation, useFocusEffect } from '@react-navigation/native';
import { fetchProducts, addProductToBasket, getCurrentBasketDetails } from '../services/AddProductService';

const AddProductScreen = () => {
  const [products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [basketItemCount, setBasketItemCount] = useState(0);
  const navigation = useNavigation();

  const fetchBasketItemCount = async () => {
    try {
      const basketDetailsResult = await getCurrentBasketDetails();

      if (basketDetailsResult.success) {
        setBasketItemCount(basketDetailsResult.data.length);
      } else {
        Alert.alert('Error', basketDetailsResult.message);
      }
    } catch (error) {
      console.error('Error fetching basket item count:', error.message);
      Alert.alert('Error', 'An unexpected error occurred while fetching basket item count.');
    }
  };

  useFocusEffect(
    React.useCallback(() => {
      fetchBasketItemCount();
    }, [])
  );

  useEffect(() => {
    const fetchData = async () => {
      const productResult = await fetchProducts();
      const basketDetailsResult = await getCurrentBasketDetails();

      if (productResult.success && basketDetailsResult.success) {
        const initialQuantities = {};
        productResult.data.forEach((product) => {
          initialQuantities[product.id] = '0';
        });

        setQuantities(initialQuantities);
        setProducts(productResult.data);
        setBasketItemCount(basketDetailsResult.data.length);
      } else {
        Alert.alert('Error', productResult.message || basketDetailsResult.message);
      }
    };

    fetchData();
  }, []);

  const handleQuantityChange = (productId, text) => {
    setQuantities((prev) => ({ ...prev, [productId]: text }));
  };

  const handleCheckBasket = () => {
    navigation.navigate('CompleteOrderScreen');
  };

  const handleAddProduct = async (productId) => {
    const quantity = quantities[productId];

    if (!quantity || isNaN(quantity) || quantity <= 0) {
      Alert.alert('Invalid Quantity', 'Please enter a valid quantity greater than zero.');
      return;
    }

    const result = await addProductToBasket(productId, quantity);
    if (result.success) {
      Alert.alert('Success', result.message);
      fetchBasketItemCount();
    } else {
      Alert.alert('Error', result.message);
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.basketItemsContainer}>
        <Text style={styles.basketItemsText}>{`Basket Items: ${basketItemCount}`}</Text>
      </View>
      <FlatList
        data={products}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={styles.productContainer} key={item.id}>
            <Text style={styles.productText}>{`Product Name: ${item.productName}`}</Text>
            <Text style={styles.productText}>{`Product Code: ${item.productCode}`}</Text>
            <Text style={styles.productText}>{`Unit Price: ${item.unitPrice} ₺`}</Text>
            <View style={styles.quantityContainer}>
              <Text style={styles.quantityText}>Quantity:</Text>
              <TextInput
                style={styles.quantityInput}
                value={quantities[item.id]}
                onChangeText={(text) => handleQuantityChange(item.id, text)}
                placeholder="Enter quantity"
                keyboardType="numeric"
              />
            </View>
            <Button
              style={styles.addButton}
              title="Add to Basket"
              onPress={() => handleAddProduct(item.id)}
              color="blue"
            />
          </View>
        )}
      />
      <View>
        <TouchableOpacity onPress={handleCheckBasket} style={styles.addButton}>
          <Text style={styles.addButtonText}>Check Basket</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};


const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    justifyContent: 'space-between',
  },
  basketItemsContainer: {
    backgroundColor: '#3498db',
    padding: 10,
    borderRadius: 5,
    alignItems: 'center',
    marginBottom: 10,
  },
  basketItemsText: {
    color: '#fff',
    fontSize: 18,
  },
  productContainer: {
    flex: 1,  
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 8,
    padding: 16,
    marginVertical: 8,
  },
  productText: {
    marginBottom: 5,
    fontSize: 14,
    fontWeight: 'bold',
    color: '#333333'
  },
  quantityContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 10,
  },
  quantityText: {
    marginRight: 10,
    fontSize: 14,
    fontWeight: 'bold',
    color: '#333333'
  },
  quantityInput: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    flex: 1,
    paddingLeft: 10,
  },
  addButton: {
    backgroundColor: 'green',
    padding: 10,
    borderRadius: 8,
    marginTop: 16,
  },
  addButtonText: {
    color: 'white',
    textAlign: 'center',
    fontSize: 16,
    fontWeight: 'bold',
  },
});


export default AddProductScreen;
