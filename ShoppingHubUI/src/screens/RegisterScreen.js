import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, Alert, StyleSheet, Button } from 'react-native';
import registerService from '../services/RegisterService';

const RegisterScreen = ({ navigation }) => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');

  const handleRegister = async () => {
    const registerData = {
      userName,
      password,
      email,
      firstName,
      lastName,
      phoneNumber,
    };

    const result = await registerService(registerData);

    if (result.success) {
      Alert.alert('Registration Successful', 'User registered successfully');
      navigation.goBack(); // Navigate back to the login screen
    } else {
      Alert.alert('Registration Failed', result.message);
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Register</Text>
      <TextInput
        style={styles.input}
        value={userName}
        onChangeText={(text) => setUserName(text)}
        placeholder="Enter username"
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        value={password}
        onChangeText={(text) => setPassword(text)}
        placeholder="Enter password"
        secureTextEntry
      />
      <TextInput
        style={styles.input}
        value={email}
        onChangeText={(text) => setEmail(text)}
        placeholder="Enter email"
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        value={firstName}
        onChangeText={(text) => setFirstName(text)}
        placeholder="Enter first name"
      />
      <TextInput
        style={styles.input}
        value={lastName}
        onChangeText={(text) => setLastName(text)}
        placeholder="Enter last name"
      />
      <TextInput
        style={styles.input}
        value={phoneNumber}
        onChangeText={(text) => setPhoneNumber(text)}
        placeholder="Enter phone number"
      />
      <TouchableOpacity style={styles.button} onPress={handleRegister}>
        <Text style={styles.buttonText}>Register</Text>
      </TouchableOpacity>
      <Button title="Already have an account?" onPress={() => navigation.goBack()} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
  },
  header: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    marginBottom: 12,
    padding: 8,
    width: '100%',
  },
  button: {
    backgroundColor: '#3498db',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 5,
    marginTop: 10,
  },
  buttonText: {
    color: '#fff', 
    fontWeight: 'bold',
    textAlign: 'center',
  },
});

export default RegisterScreen;
