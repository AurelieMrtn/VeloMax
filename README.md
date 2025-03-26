# VéloMax Database Management System

## Overview
This is a school project originally written in French. It creates a dummy company and aims to create its database system. You can find the original instructions in "Sujet.pdf".
VéloMax is a company specializing in selling bicycles and spare parts. Due to a significant increase in sales, the company requires a new database system to efficiently manage orders, customers, suppliers, and inventory.

## Project Scope
The project consists of developing a database using MySQL and a management application in C# with WPF. The application will support inventory management, order tracking, customer relations, and supplier interactions.

## Features
### 1. Bicycle and Spare Parts Management
- **Create, update, and delete bicycles and spare parts** while maintaining data consistency.
- Track model introduction and discontinuation dates.
- Manage suppliers offering the same spare parts with different pricing and availability.

### 2. Customer Management
- **Boutiques and Individual Clients**:
  - Store customer details, including contact information.
  - Manage commercial discounts for boutiques based on purchase volume.
  - Implement the **Fidélio membership program** for individuals, offering discounts on purchases.

### 3. Supplier Management
- Maintain supplier records with details such as SIRET number, contact information, and reliability rating.
- Prioritize suppliers based on responsiveness.

### 4. Order Processing
- Record customer orders, including bicycles and spare parts.
- Track delivery dates and notify customers of stock shortages.
- Estimate delivery time based on supplier lead times.

### 5. Inventory and Stock Monitoring
- Maintain real-time stock levels for bicycles and spare parts.
- Generate alerts for low stock levels and suggest supplier restocking options.

### 6. Statistical Reports
- Generate sales reports, including total quantities sold.
- List active members of the **Fidélio program** and their subscription expiration dates.
- Identify top customers based on purchase history.
- Analyze order trends, including average order value and items per order.

## Technical Implementation
### 1. Database Design
- The database will be implemented in **MySQL** under the schema `VeloMax`.
- It will include tables for bicycles, spare parts, suppliers, orders, and customers.
- The database will have constraints to maintain referential integrity.

### 2. Application Development
- The application will be developed in **C#** with **WPF**.

### 3. Data Export
- **XML export** for low-stock items and supplier details.
- **JSON export** for customers whose **Fidélio membership** is about to expire.

### 4. Custom Queries
The system will support:
- **Synchronized queries**
- **Self-joins**
- **Union queries**

## License
This project is part of an academic exercise and is not intended for commercial use.

