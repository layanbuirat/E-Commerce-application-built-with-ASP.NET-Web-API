# E-Commerce Application

## Project Overview
ASP.NET Web API E-Commerce system with authentication, products, cart, and checkout.

## Technologies
- ASP.NET Core 9.0
- Entity Framework Core
- SQL Server
- JWT Authentication

## How to Run
1. Update connection string in appsettings.json
2. Run migrations: `dotnet ef database update`
3. Run: `dotnet run --project SHOP.PL`

## API Endpoints
- POST /api/auth/register
- GET /api/products
- POST /api/cart/add
- POST /api/checkout

cd SHOP.DAL
dotnet ef migrations add FinalMigration
dotnet ef database update

dotnet ef migrations script -o DatabaseScript.sql

cd SHOP.PL
dotnet run
dotnet clean

dotnet publish SHOP.PL -c Release -o ./PublishOutput
