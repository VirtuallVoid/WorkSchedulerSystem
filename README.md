# WorkSchedulerSystem

A full-stack scheduling management demo built with **.NET 8**,
**Feature-Based Clean Architecture**, **MediatR**, **Dapper**, and a
modern **Angular 18** frontend.\
Designed  with clean modularity, secure authentication, 
and **full RBAC (Role-Based Access Control)** enforced
both on API and frontend routing.

------------------------------------------------------------------------

DATABASE FOLDER
---------------

The `database/` directory is a lightweight replica of the database used by the API.
It contains the SQL artifacts required by the application:

 - Table definitions
 - Schemas
 - Stored procedures

This folder is intended for demonstration and development purposes only.
It allows reviewers to understand the database structure without requiring access 
to an external SQL server or production environment.

------------------------------------------------------------------------

# Backend (ASP.NET Core 8) --- API Service

A clean backend demonstrating real-world architecture patterns used in
enterprise systems.

## Features

-   Feature-Based Clean Architecture\
-   Application / Domain / Infrastructure / WebApi layers\
-   CQRS with MediatR\
-   Dapper repositories\
-   FluentValidation (optional integration)\
-   AutoMapper Mappings\
-   Global Exception Handling\
-   Dependency Injection\
-   SQL Database\
-   JWT Authentication\
-   **RBAC: Role-Based Authorization inside API**\
-   Swagger UI\
-   Modular feature slices: Auth, Schedules

------------------------------------------------------------------------

## RBAC --- Role-Based Access Control (Backend & API)

The system implements RBAC:

### Controller-Level Enforcement

``` csharp
[Authorize(Roles = "Admin")]
[Authorize(Roles = "Worker")]
```

### Feature-Level Rules

-   **Admin**
    -   Approve / Reject schedules\
    -   View all schedules\
-   **Worker**
    -   Submit schedules\
    -   View only their own schedules

API-level RBAC ensures nobody can bypass permission checks, even by
calling the API directly.

------------------------------------------------------------------------

## Backend Project Structure

    WorkSchedulerSystem/
    │
    │── Application/
    │   ├── Behaviours/
    │   ├── DTOs/
    │   ├── Enums/
    │   ├── Exceptions/
    │   ├── Features/
    │   │   ├── Auth/
    │   │   │   └── Commands/
    │   │   │
    │   │   └── Schedules/
    │   │       ├── Commands/
    │   │       └── Queries/
    │   │
    │   ├── Helpers/
    │   ├── Interfaces/
    │   │   ├── Repositories/
    │   │   ├── Services/
    │   │   ├── IDatabaseConfig.cs
    │   │   ├── IUnitOfWork.cs
    │   │   └── IUserContext.cs
    │   │
    │   ├── Mappings/
    │   ├── Wrappers/
    │   └── ServiceExtensions.cs
    │
    │── Domain/
    │   ├── Common/
    │   ├── Entities/
    │   └── Settings/
    │
    │── Infrastructure/
    │   ├── Repositories/
    │   ├── Services/
    │   ├── DatabaseConfig.cs
    │   ├── ServiceRegistration.cs
    │   └── UserContext.cs
    │
    │── WebApi/
        ├── Connected Services/
        ├── Dependencies/
        ├── Properties/
        ├── Controllers/
        ├── Extensions/
        ├── Middlewares/
        ├── appsettings.json
        │    └── appsettings.Development.json
        │
        └── Program.cs

------------------------------------------------------------------------

## API Endpoints

### Auth

  Method   Endpoint               Description
  -------- ---------------------- --------------------------------------
  POST     `/api/auth/login`      Login & retrieve JWT (includes role)
  POST     `/api/auth/register`   Register new Admin/Worker

### Schedules

  Method   Endpoint                   Role
  -------- -------------------------- --------
  GET      `/api/schedules/jobs`            All User
  GET      `/api/schedules/shift-types`     All User
  GET      `/api/schedules/my-schedules`    Worker
  GET      `/api/schedules/all`             Admin
  POST     `/api/schedules/submit`          Worker
  PUT     `/api/schedules/approve`          Admin
  PUT     `/api/schedules/reject`           Admin

------------------------------------------------------------------------

## Backend Tech Stack

-   .NET 8\
-   ASP.NET Core Web API\
-   Feature-Based Clean Architecture\
-   MediatR\
-   Dapper\
-   AutoMapper\
-   FluentValidation\
-   JWT Authentication\
-   RBAC Authorization\
-   Swagger / Swashbuckle\
-   SQL Database

------------------------------------------------------------------------

## Swagger UI

Once the API is running:

    https://localhost:44303/swagger/index.html

Swagger provides: - Interactive documentation\
- Request/Response schema\
- **Authorize → Bearer token input**\
- Role-based endpoint testing

------------------------------------------------------------------------

# Frontend (Angular 18)

frontend built with Angular 18 and powered by Node.js 20.

## Frontend Features

-   Angular 18\
-   Node.js 20 runtime\
-   Standalone routing\
-   Reactive forms\
-   Shared UI components\
-   Auth & Role Guards\
-   JWT stored in localStorage\
-   Auto-attached Authorization header\
-   Feature modules: Admin, Worker, Auth\
-   Clean and scalable folder structure

------------------------------------------------------------------------

## Frontend Project Structure

    frontend/
    │── src/
    │   ├── app/
    │   │   ├── core/
    │   │   │   ├── guards/
    │   │   │   │   ├── auth.guard.ts
    │   │   │   │   └── role.guard.ts
    │   │   │   ├── interceptors/
    │   │   │   │   └── auth.interceptor.ts
    │   │   │   ├── services/
    │   │   │       ├── auth.service.ts
    │   │   │       ├── admin.service.ts
    │   │   │       └── worker.service.ts
    │   │   ├── features/
    │   │   │   ├── admin/
    │   │   │   ├── auth/
    │   │   │   ├── worker/
    │   │   ├── shared/
    │   │   │   ├── logout-button/
    │   │   │   └── models/
    │   │   └── app.routes.ts
    │   ├── environments/
    │   ├── main.ts
    │   ├── styles.scss

------------------------------------------------------------------------

## Frontend Tech Stack

-   Angular 18\
-   Node.js 20\
-   TypeScript\
-   SCSS\
-   Angular Router\
-   JWT Auth\
-   Guards + Interceptors\
-   Feature Modules (Admin/Worker/Auth)

------------------------------------------------------------------------
