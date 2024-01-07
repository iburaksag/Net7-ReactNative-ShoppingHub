# ShoppingHub

ShoppingHub is a full-stack shopping cart application that enables users to add products to basket, manage their shopping carts, and place the orders. The project is built using .NET Core for the backend API with Clean Architecture principles and React Native Expo for the frontend.

## Features

- **User Authentication:** Users can register and log in securely to access personalized shopping features.
- **Shopping Cart:** User can add, remove, and view products to the basket.
- **Order Creation:** Finalize the basket and create an order for the logged user.
- **Basket List:** View a list of created baskets for user in Basket List Screen.
- **Basket Details:** Access detailed information about a specific basket.

## Tech Stack

- **Backend:**
  - .NET Core Web API
  - Clean Architecture with Domain, Application, Infrastructure, and Presentation layers.
  - CQRS Pattern for better separation of concerns.
  - Generic Repository and Unit of Work patterns for data access.
  - PostgreSQL for the relational database.
  - Fluent Validation for input validation.
  - Serilog for logging.

- **Frontend:**
  - React Native Expo for cross-platform mobile development.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/)
- [Expo CLI](https://docs.expo.dev/get-started/installation/)

### Backend Setup

1. Clone the repository.
   ```bash
   git clone https://github.com/iburaksag/dotnet7-ShoppingHub.git
   cd ShoppingHub/Backend

2. Create a PostgreSQL database and update the connection string in appsettings.json.
   
3. Apply migrations to create the database
   ```bash
   dotnet ef database update
   
4. Run the API.
   ```bash
   dotnet run

### Frontend Setup

1. Navigate to the Frontend directory.
   ```bash
   cd ../ShoppingHubUI
   
2. Install dependencies.
   ```bash
   npm install

3. Start the Expo project.
   ```bash
   npx expo start   

## Usage

1. Register or log in to start shopping.
2. Create a new basket and add products to the basket.
3. Review and finalize your cart to create an order.
4. Click Complete Order and create the basket.
5. Explore the list of created baskets and view details if needed.

