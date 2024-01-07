# ShoppingHub

ShoppingHub is a full-stack e-commerce application that enables users to shop for products, manage their shopping carts, and place orders. The project is built using .NET Core for the backend API with Clean Architecture principles and React Native Expo for the frontend.

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
   git clone https://github.com/your-username/ShoppingHub.git
   cd ShoppingHub/Backend

2. Create a PostgreSQL database and update the connection string in appsettings.json.
3. Apply migrations to create the database
4. 

