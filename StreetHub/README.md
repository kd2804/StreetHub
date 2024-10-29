# StreetHub API
StreetHub is a RESTful API designed for managing street data, including street information and geometrical data using .NET and Entity Framework Core. This API utilizes PostgreSQL with PostGIS for spatial data handling.

## Table of Contents
- [Features](#features)
- [Technologies](#technologies)
- [Setup](#setup)
- [Endpoints](#endpoints)
- [Contributing](#contributing)

## Features
- Create, read, update, and delete street information.
- Manage street geometries using spatial data types.
- Swagger UI for API documentation and testing.

## Technologies
- .NET 8.0
- ASP.NET Core
- Entity Framework Core
- PostgreSQL with PostGIS
- Swagger for API documentation

## Setup
### Prerequisites
- .NET SDK (6.0 or later)
- PostgreSQL with PostGIS
- Docker (optional, for containerized setup)

### Database Setup
- Create the Database: Set up a PostgreSQL database named streetdb and ensure PostGIS is enabled.
- Connection String: Update the connection string in Program.cs if necessary.

### Build and Run
Restore Dependencies:

bash
dotnet restore
Run Migrations (if using EF Core):

bash
dotnet ef database update
Run the Application:

bash
dotnet run
Access the API: Open your browser and go to http://localhost:8080 to access the Swagger UI.

### Clone the Repository
```bash
git clone https://github.com/yourusername/streethub.git
cd streethub