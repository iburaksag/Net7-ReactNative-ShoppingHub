import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, TouchableOpacity, StyleSheet, Button } from 'react-native';
import { useNavigation, useFocusEffect } from '@react-navigation/native';
import basketListService from '../services/BasketListService';

const BasketListScreen = () => {
  const [baskets, setBaskets] = useState([]);
  const [error, setError] = useState(null);
  const navigation = useNavigation();

  const fetchBaskets = async () => {
    const result = await basketListService.getBasketsByUserId();

    if (result.success) {
      setBaskets(result.baskets);
    } else {
      setError(result.message);
    }
  };

  useEffect(() => {
    fetchBaskets();
  }, []);

  useFocusEffect(
    React.useCallback(() => {
      fetchBaskets();
    }, [])
  );

  const handleBasketClick = (basketId) => {
    navigation.navigate('BasketInfoScreen', { basketId });
  };


  const handleAddOrder = async () => {
    const result = await basketListService.createBasket();

    if (result.success) {
      navigation.navigate('AddProductScreen', { basketId: result.basketId });
    } else {
      console.error('Failed to create a new basket:', result.message);
    }
  };

  return (
    <View style={styles.container}>
      <FlatList
        data={baskets}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <TouchableOpacity onPress={() => handleBasketClick(item.id)}>
            <View style={styles.basketItem}>
              <Text style={styles.basketProp}>{`Basket ID: ${item.id}`}</Text>
              <Text style={styles.basketProp}>{`Order Address: ${item.orderAddress}`}</Text>
              <Text style={styles.basketProp}>{`Order Date: ${formatOrderDate(item.orderDate)}`}</Text>
              <Text style={styles.orderTotal}>{`Order Total: ${item.orderTotal.toFixed(2)} ₺`}</Text>
            </View>
          </TouchableOpacity>
        )}
      />
        {error && <Text style={styles.errorText}>{error}</Text>}
        <TouchableOpacity onPress={handleAddOrder} style={styles.addButton}>
        <Text style={styles.addButtonText}>Add Order</Text>
        </TouchableOpacity>
    </View>
  );
};

const formatOrderDate = (orderDate) => {
  const date = new Date(orderDate);
  const formattedDate = `${date.getDate()}-${date.getMonth() + 1}-${date.getFullYear()}`;
  return formattedDate;
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  basketItem: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 8,
    padding: 16,
    marginVertical: 8,
  },
  basketProp: {
    marginBottom: 3,
    fontSize: 13,
    fontWeight: 'bold',
    color: '#444444'
  },
  orderTotal: {
    fontWeight: 'bold',
    marginTop: 8,
    fontSize: 16,
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
  errorText: {
    color: 'red',
    marginTop: 8,
  },
});

export default BasketListScreen;
