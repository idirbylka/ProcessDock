# ProcessDock

ProcessDock is a backend Web API built with ASP.NET Core for managing business operations and workflows.

The goal of this project is to simulate a real-world backend system where companies can manage projects, tasks, and operational processes in a structured and scalable way.

This project is being developed step-by-step with a focus on:
- clean architecture
- proper data modeling
- real-world backend practices
- long-term scalability

> Note: The project initially started with a "Workspace" concept as the root structure.  
> During development, the domain model was refined to focus on a project-centered architecture.  
> This reflects an intentional design evolution based on better understanding of the problem domain.

---

## Current Features

### Initial Foundation
- Basic API setup with ASP.NET Core
- EF Core integration with SQL Server (Docker)
- Initial Workspace module (early domain exploration)
- Project Module (in progress)

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

- Project management (core entity)
- Task management within projects
- Time tracking (work sessions)
- Operational metrics (e.g., processed items, effort)
- Cost calculation
- Improved validation and error handling
- Service layer for business logic
- Authentication and authorization
- Modular architecture for future extensions (e.g., barcode systems, integrations)


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