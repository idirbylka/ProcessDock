# ProcessDock

ProcessDock is a backend Web API built with ASP.NET Core for managing business operations and workflows.

The goal of this project is to simulate a real-world backend system where companies can manage projects, tasks, and operational processes in a structured, scalable, and maintainable way.

This project is developed incrementally with a focus on:

* clean architecture principles
* proper data modeling
* separation of concerns
* real-world backend practices
* long-term scalability



---

## Current Features

### Core Modules

#### Project

* Create, update, delete projects
* Represents the main business container

#### WorkItem (Task)

* Belongs to a project
* Represents a unit of work within a project

#### WorkSession (Time Tracking)

* Start / stop work sessions on a WorkItem
Enforces one active session per WorkItem (V1 rule)
* Tracks execution time
* Calculates duration automatically when a session is stopped
* Encapsulates lifecycle behavior in the WorkSession entity

---

## Architecture Overview

The application follows a layered architecture:

* Controllers → HTTP/API layer
* Application → Services, DTOs, use-case orchestration
* Domain → Entities, Enums, domain behavior
* Infrastructure → Database configuration and EF Core persistence

The WorkSession module introduces a service layer and interface abstraction, separating business logic from request handling.

---

## Tech Stack

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server (Docker)
* Swagger / OpenAPI

---

## Database

* SQL Server running in Docker
* EF Core for ORM and migrations
* Persistent Docker volume for data storage

---

## Project Structure

```text id="c4mfcm"
Controllers/         → API endpoints
Application/
  ├── DTOs/          → Request / Response models
  ├── Services/      → Business logic
  └── DependencyInjection.cs
Domain/
  ├── Entities/      → Core models
  └── Enums/
Infrastructure/
  ├── Data/          → DbContext
  └── DependencyInjection.cs
Program.cs           → Application entry point
docs/                → Project documentation
```

---

## How to Run Locally

### 1. Run SQL Server in Docker

```bash id="b8lqbo"
docker run -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 \
  --name processdock-sql \
  -v processdock-sql-data:/var/opt/mssql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

---

### 2. Update connection string

In `appsettings.Development.json`:

```json id="d0x5v8"
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ProcessDockDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
}
```

---

### 3. Apply migrations

```bash id="9b7n9x"
dotnet ef database update
```

---

### 4. Run the API

```bash id="n8jz0q"
dotnet run
```

Then open Swagger:

```bash id="yq5m6m"
https://localhost:<port>/swagger
```

---

## Current Business Rules (V1)

* A WorkItem belongs to one Project
* A WorkSession belongs to one WorkItem
* Only one active WorkSession per WorkItem is allowed

> Note: This rule is intentionally simplified for the initial version and may evolve to support multi-user collaboration.

---

## Future Plans

ProcessDock is designed to grow incrementally into a modular system for managing business operations and workflows.

The current focus is on establishing a strong foundation around:

* project and task management
* execution tracking (work sessions)
* clean architecture and domain modeling

Planned next steps include:

* Authentication and authorization
* Improved validation and error handling
* WorkItem lifecycle management
* Aggregation of operational data (e.g. durations, metrics)

As the system evolves, it will expand to support broader operational workflows, including:

* workflow coordination across different areas
* reporting and insights
* integrations with external systems

The long-term direction is to explore how a modular backend can support multiple business needs within a unified architecture.

> Note: The system evolves step-by-step, prioritizing maintainability and design clarity over rapid feature expansion.

---

## Purpose of This Project

This project is part of my journey to:

* strengthen backend development skills in .NET
* build production-like APIs
* apply clean architecture principles
* create a strong portfolio for software engineering roles

---

## Notes

## Notes

This is an actively evolving project.  
The structure and capabilities will continue to evolve as new requirements are introduced.

The project initially started with a Workspace concept, but the active domain model has since been refined into a project-centered structure. The legacy Workspace module was removed from the active codebase to keep the current model focused and consistent.
---

## Author

Idir
