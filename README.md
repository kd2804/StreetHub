
# StreetHub

**StreetHub** is a .NET Core REST API designed to manage and modify street-related data, including street names, geometries, and vehicle capacities. It utilizes **PostgreSQL** with **PostGIS** for spatial data storage and **Entity Framework Core** as the ORM. The project is containerized with **Docker** and includes configurations for **Kubernetes** deployment.

---

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Running the Project](#running-the-project)
- [API Usage](#api-usage)
- [Technology Stack](#technology-stack)
- [Future Enhancements](#future-enhancements)

---

## Features

- **Street Management**: Create and delete street data.
- **Geometry Manipulation**: Add points to street geometry.
- **Race Condition Prevention**: Handle concurrent modifications to ensure data consistency.
- **Feature Flag**: Choose between in-database and backend-based geometry processing.
- **Containerization**: Docker and Docker Compose for local setup, Kubernetes for production deployment.

---

## Project Structure

```plaintext
├── Controllers               # API Controllers
├── Services
│   ├── Contracts             # Service interfaces
│   └── Implementations       # Service implementations
├── Repositories
│   ├── Contracts             # Repository interfaces
│   └── Implementations       # Repository implementations
├── Models                    # Entity and DTO models
├── Data                      # Database context and migrations
├── Infrastructure            # Configuration and extensions
├── Dockerfile                # Docker configuration
├── docker-compose.yml        # Docker Compose setup
├── k8s                       # Kubernetes manifest files
└── README.md                 # Project documentation
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) with **PostGIS extension**
- [Docker](https://www.docker.com/products/docker-desktop)
- [Kubernetes CLI (kubectl)](https://kubernetes.io/docs/tasks/tools/)

---

## Setup

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/StreetHub.git
   cd StreetHub
   ```

2. **Configure Environment Variables**

   Add environment variables to the `appsettings.json` or set them up in a `.env` file (Docker Compose).

   - **PostgreSQL connection string**:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Host=postgres;Port=5432;Database=streetdb;Username=yourusername;Password=yourpassword"
     }
     ```

3. **Database Setup and Migrations**

   Use Entity Framework Core to create and update the database schema.

   - **Install EF CLI (if needed)**:
     ```bash
     dotnet tool install --global dotnet-ef
     ```

   - **Create Initial Migration**:
     ```bash
     dotnet ef migrations add InitialCreate -p StreetHub -s StreetHub
     ```

   - **Apply Migrations**:
     ```bash
     dotnet ef database update -p StreetHub -s StreetHub
     ```

   - **Adding New Migrations**: If you make changes to models later, use:
     ```bash
     dotnet ef migrations add <MigrationName> -p StreetHub -s StreetHub
     ```

4. **Docker Setup**

   - Build and run the Docker container locally:

     ```bash
     docker-compose up --build
     ```

---

## Running the Project

### Locally with .NET CLI

1. **Run the API**

   ```bash
   dotnet run
   ```

2. **Access the API**

   Open your browser or use Postman to access `http://localhost:8000/api/streets`.

### Using Docker

To run the project in Docker, use:

```bash
docker-compose up
```

### Kubernetes Deployment

For Kubernetes deployment, apply the manifest files:

```bash
kubectl apply -f k8s/deployment.yml
```

---

## API Usage

### Endpoints

- **POST** `/api/streets` - Create a new street.
- **DELETE** `/api/streets/{id}` - Delete a street by ID.
- **POST** `/api/streets/{id}/addPoint` - Add a point to a street geometry.
- **GET** `/api/streets/{id}` - Retrieve a street by ID.

### Example Requests

#### Create a New Street

```http
POST /api/streets
Content-Type: application/json

{
  "name": "Main Street",
  "geometry": {
    "type": "LineString",
    "coordinates": [[0, 0], [1, 1]]
  },
  "capacity": 50
}
```

#### Add a Point to Geometry

```http
POST /api/streets/1/addPoint
Content-Type: application/json

{
  "point": [2, 2],
  "position": "end" // Options: "start" or "end"
}
```

#### Get a Street by ID

```http
GET /api/streets/1
```

**Response:**

```json
{
  "id": 1,
  "name": "Main Street",
  "geometry": {
    "type": "LineString",
    "coordinates": [[0, 0], [1, 1], [2, 2]]
  },
  "capacity": 50
}
```

---

## Technology Stack

- **.NET Core 8**: Application framework
- **Entity Framework Core**: ORM for data access
- **PostgreSQL + PostGIS**: Database for street data with spatial data support
- **NetTopologySuite**: Spatial data types and operations
- **Docker / Kubernetes**: Containerization and orchestration

---

## Future Enhancements

- **Authentication & Authorization**: Secure the API with JWT or OAuth.
- **Logging & Monitoring**: Integrate logging and monitoring tools.
- **Testing**: Unit and integration tests for controllers and services.

---
