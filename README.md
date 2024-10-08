# Orders Challenge

## Microservice challenge - Orders and Customers

> IMPORTANT: Docker is not used in this project because I recently updated my operating system to Windows 11 Home, which does not support Docker Desktop. Resolving this issue is a priority, and implementing Docker will be the first thing on my wish list once it is addressed.

This project demonstrates a microservices-based order processing system using .NET, RabbitMQ, PostgreSQL, and Dapper/Entity Framework. The system consists of three main components:

1. **Order.Publisher**: A console application that publishes order messages to RabbitMQ.
2. **Service.Order**: A web API that subscribes to RabbitMQ messages, processes them, and saves orders to the database using Dapper.
3. **Service.OrderManagement**: A web API providing endpoints to retrieve order-related information using Entity Framework.

### Project Structure

- **Order.Publisher**: Console application to publish order messages.
- **Service.Order**: Web API to process and store orders.
- **Service.OrderManagement**: Web API to provide customer order information.

### Technologies Used
- .NET Core
- RabbitMQ
- PostgreSQL
- Dapper (Service.Order)
- Entity Framework Core (Service.OrderManagement)

### Getting Started

#### Prerequisites - Libraries
- .NET 7
- RabbitMQ.Client 6.8.1
- Dapper 2.1.35
- Newtonsoft.Json 13.0.3
- Npgsql 8.0.3
- Microsoft.EntityFrameworkCore 7.0.20
- Npgsql.EntityFrameworkCore.PostgreSQL 7.0.18

#### Setting Up PostgreSQL

1. Install PostgreSQL:
- [Download and install PostgreSQL.](https://www.postgresql.org/download/)

2. Create Database:
- Open your terminal or PostgreSQL shell and run:
```sql
CREATE DATABASE orderdb;
```

3. Create Tables:
- Run the following SQL script to create the necessary tables:

```sql
CREATE TABLE customers (
    customerid SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE orders (
    orderid SERIAL PRIMARY KEY,
    code VARCHAR(100) NOT NULL,
    customerid INT NOT NULL,
    totalvalue DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (customerid) REFERENCES customers(customerid)
);

CREATE TABLE orderitems (
    orderitemid SERIAL PRIMARY KEY,
    orderid INT NOT NULL,
    product VARCHAR(100) NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (orderid) REFERENCES orders(orderid)
);

-- insert dummy data
INSERT INTO customers (name) VALUES ('Customer 1');
INSERT INTO customers (name) VALUES ('Customer 2');
```

### RabbitMQ Setup

1. Install RabbitMQ:
- [Download and install RabbitMQ.](https://www.rabbitmq.com/docs/download)

2. Start RabbitMQ:
- Ensure RabbitMQ is running:
```bash
rabbitmq-server
```

### Running the Project

1. Order.Publisher: (to save the data correctly in the Service.Order, you should send a valid customerid here e.g.: 1 or 2).
- Navigate to the Order.Publisher folder.
- Restore dependencies and run the application:

```bash
dotnet restore
dotnet run
```

2. Service.Order:
- Navigate to the Service.Order/Service.Order.API folder.
- Restore dependencies and run the application:

```bash
dotnet restore
dotnet run
```

3. Service.OrderManagement:
- Navigate to the Service.OrderManagement/Service.OrderManagement.API folder.
- Restore dependencies and run the application:

```bash
dotnet restore
dotnet run
```

### API Endpoints

**Service.Order**:
- This service subscribes to RabbitMQ messages and processes orders.

**Service.OrderManagement**:

- Retrieve total value of an order:
```bash
GET /api/orders/{orderId}/total-value
```

- Retrieve the count of orders by a customer:
```bash
GET /api/customers/{customerId}/orders-count
```

- Retrieve the list of orders by a customer:
```bash
GET /api/customers/{customerId}/orders
```

### Publisher Configuration
**Order.Publisher** uses RabbitMQ.Client library to publish messages to RabbitMQ.

### Subscriber Configuration
**Service.Order** subscribes to the order messages from RabbitMQ and processes them.

> By following these steps, you should be able to set up and run the project locally. If you encounter any issues, please refer to the documentation or open an issue in the repository.

### Tech Report

Access the [technical report](https://docs.google.com/document/d/1-MTEprrOzgiwaEpI-lBqW46dcLWQYjpWC5mVX--2qkM/edit?usp=sharing) for additional information.
