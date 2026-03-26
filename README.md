# ProcessDock

ProcessDock is a backend Web API built with ASP.NET Core for managing business workflows within a workspace-based structure.

The goal of this project is to simulate a real-world business operations system, where companies can manage their processes (projects, tasks, items, etc.) inside isolated workspaces.

This project is being developed step-by-step with a focus on clean architecture, scalability, and real-world backend practices.

---

## Current Features

### Workspace Module (v1)
- Create a workspace
- Retrieve all workspaces
- Retrieve a workspace by ID
- Update a workspace
- Delete a workspace

---

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Docker)
- Swagger / OpenAPI

---

## Database

- SQL Server running in Docker
- EF Core used for ORM and migrations
- Persistent Docker volume for data storage

---

## Project Structure

- `Controllers/` → API endpoints
- `Models/` → Domain models
- `Data/` → Database context (EF Core)
- `Program.cs` → Application configuration

---

## How to Run Locally

### 1. Run SQL Server in Docker

```bash
docker run -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 \
  --name processdock-sql \
  -v processdock-sql-data:/var/opt/mssql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

---

### 2. Update connection string

In appsettings.Development.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ProcessDockDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
}
```

---

### 3. Apply migrations

```bash
dotnet ef database update
```
---

### 4. Run the API
```bash
dotnet run
```

Then open Swagger:

```bash
https://localhost:<port>/swagger
```

---

## Future Plans

This project is designed to evolve into a modular business operations platform.

Planned next features:

- Project and task management
- Time tracking
- Validation and error handling improvements
- Service layer (business logic separation)
- Authentication and authorization
- Dockerized application (API + DB)
- Scalable architecture improvements


---

## Purpose of This Project

This project is part of my journey to:

- strengthen backend development skills in .NET
- build production-like APIs
- apply clean architecture principles
- create a strong portfolio for software engineering roles

## Notes

This is an actively evolving project. Features and structure will improve over time as the system grows.

---

## Author

Idir