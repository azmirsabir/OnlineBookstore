# 📚 Online Bookstore API

This is an ASP.NET Core Web API project for an online bookstore, built as part of an assignment. It supports user registration/login, user management by admin, book browsing, cartItem, Shipping info, order placement, and admin management.

## 🚀 Features

- JWT Authentication & Role-based Authorization  
- Admin & User separation  
- Book & Order, User management  
- Rate Limiting  
- Middleware for logging and exception handling  
- Swagger/OpenAPI support  
- MySQL + EF Core integration  

## 🛠️ Setup Instructions

1. Clone the repository: `git clone git@github.com:azmirsabir/OnlineBookstore.git && cd OnlineBookstore`  
2. Create a MySQL database: `CREATE DATABASE online_bookstore;` (via MySQL CLI or GUI)  
3. Open `appsettings.json` and update connection string:  
   `"DefaultConnection": "Server=localhost;Database=online_bookstore;User=root;Password=yourpassword;"`  
4. (Optional) Install EF Core CLI: `dotnet tool install --global dotnet-ef`  
5. Apply migrations: `dotnet ef database update`  
6. Run the app: `dotnet run`  
7. Access Swagger UI at `http://localhost:5290/swagger/index.html` 

## 📂 Project Structure

├── Controllers/        → API endpoints
├── Entities/           → Core entities
├── Models/             → DTOs and view models
├── Services/           → Business logic services
├── Data/               → EF Core DbContext & seed data
├── Extensions/         → Service registration extensions
├── Exceptions/         → Custom exception handling
├── Helpers/            → Utility and helper classes
├── Middlewares/        → Logging, exception middleware
├── Logs/               → Application logs (Serilog)
├── Migrations/         → EF Core migrations
└── Program.cs          → Application entry point

## 👥 Roles

- Admin: Full access to manage books/orders/Users  
- User: Can place/view orders, browse books, cartItem

## ✅ Prerequisites

- .NET 8/9 SDK  
- MySQL  
- dotnet-ef CLI tool  

## 📄 License

This is an project intended for assignment purposes only.

## 👤 Author

Azmir Sabir  
GitHub: [@azmirsabir](https://github.com/azmirsabir)
