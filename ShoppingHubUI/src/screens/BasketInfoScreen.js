import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, StyleSheet } from 'react-native';
import { useRoute } from '@react-navigation/native';
import { getBasketDetailsByBasketId, getProductById } from '../services/BasketInfoService';

const BasketInfoScreen = () => {
  const [basketDetails, setBasketDetails] = useState([]);
  const route = useRoute();

  useEffect(() => {
    const fetchBasketDetails = async () => {
      const basketId = route.params?.basketId;

      if (basketId) {
        const result = await getBasketDetailsByBasketId(basketId);

        if (result.success) {
          const basketDetailsWithProducts = await Promise.all(
            result.data.map(async (item) => {
              const productResult = await getProductById(item.productId);
              return {
                ...item,
                productDetails: productResult.success ? productResult.data : null,
              };
            })
          );

          setBasketDetails(basketDetailsWithProducts);
        } else {
          console.error('Error fetching basket details:', result.message);
        }
      }
    };

    fetchBasketDetails();
  }, [route.params?.basketId]);

  return (
    <View style={styles.container}>
      <Text style={styles.header}>All Products in Basket {route.params?.basketId} </Text>
      <FlatList
        data={basketDetails}
        keyExtractor={(item) => item.productId.toString()}
        renderItem={({ item }) => (
          <View style={styles.basketItem}>
            <Text style={styles.basketProp}>{`Product Name: ${item.productDetails?.productName || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Product Code: ${item.productDetails?.productCode || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Quantity: ${item.quantity}`}</Text>
            <Text style={styles.basketProp}>{`Unit Price: ${item.productDetails?.unitPrice || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Category: ${item.productDetails?.category || 'N/A'}`}</Text>
            <Text style={styles.basketProp}>{`Created Date: ${new Date(item.createdAt).toLocaleDateString('en-GB')}`}</Text>
          </View>
        )}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  header: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 16,
    textAlign: 'center'
  },
  basketItem: {
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
});

export default BasketInfoScreen;
