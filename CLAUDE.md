# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**DiarioFit** — A full-stack fitness training diary web app. Users can create training routines (mesocycles), track workout sessions, record sets/reps/weights, view progress charts, and log body measurements.

- **Backend**: .NET 9 / ASP.NET Core Web API — `ProyectoFinalGrado/`
- **Frontend**: Angular 20 (standalone components) — `ProyectoFinGradoFrontend/DiarioEntrenamiento/`
- **Database**: PostgreSQL (Neon cloud-hosted)

---

## Commands

### Backend (.NET)

```bash
# Build
cd ProyectoFinalGrado && dotnet build

# Run API (HTTP on :5156, HTTPS on :7218)
dotnet run --project ProyectoFinalGrado/src/DiarioEntrenamiento.Api

# Run all tests
cd ProyectoFinalGrado && dotnet test

# Run a specific test project
dotnet test ProyectoFinalGrado/tests/Domain.Tests
dotnet test ProyectoFinalGrado/tests/Application.Tests

# Run a single test
dotnet test --filter "FullyQualifiedName~TestClassName.MethodName"
```

### Frontend (Angular)

```bash
cd ProyectoFinGradoFrontend/DiarioEntrenamiento

# Install deps (uses Bun)
bun install

# Dev server on http://localhost:4200
ng serve

# Build for production
ng build

# Run unit tests (Karma + Jasmine)
ng test

# Run a single test file
ng test --include="**/feature-name.component.spec.ts"
```

---

## Backend Architecture

The backend follows **Clean Architecture** with **DDD** and **CQRS via MediatR**.

### Layer Responsibilities

| Layer | Project | Purpose |
|---|---|---|
| Domain | `DiarioEntrenamiento.Domain` | Entities, value objects, domain events, error definitions |
| Application | `DiarioEntrenamiento.Application` | Commands, queries, validators, abstractions/interfaces |
| Infrastructure | `DiarioEntrenamiento.Infrastructure` | EF Core, Dapper, JWT, BCrypt, email, clock |
| API | `DiarioEntrenamiento.Api` | Controllers, request DTOs, DI wiring, `Program.cs` |

### CQRS Pattern

- **Commands** (writes): implement `ICommand<TResponse>` / `ICommandHandler<TCommand, TResponse>`
- **Queries** (reads): implement `IQuery<TResponse>` / `IQueryHandler<TQuery, TResponse>`
- All handlers return `Result<T>` (a discriminated union carrying either a value or an `Error`)
- A `ValidationBehaviour` MediatR pipeline behaviour runs FluentValidation before every handler

### Domain Events

Domain events (e.g., `UsuarioCreadoDomainEvent`) are raised inside aggregate roots and dispatched after persistence via the infrastructure messaging layer.

### Persistence Strategy

- **EF Core** — writes (commands), aggregate persistence
- **Dapper** — reads (queries), raw SQL for performance
- `IUnitOfWork` wraps the EF `SaveChanges` + domain event dispatch

### Authentication

- JWT bearer tokens — symmetric key configured in `appsettings.json` under `Jwt`
- Password hashing with BCrypt via `IPasswordHasher`
- CORS configured for `http://localhost:4200`

---

## Frontend Architecture

### Key Conventions

- **Standalone components only** — no NgModules
- Routes are lazy-loaded at the feature level (see `app.routes.ts`)
- Two layout shells: `loginlayout` (auth pages) and `mainlayout` (authenticated app)
- Each feature folder contains its component + a `servicios/` subfolder with an HTTP service and response DTOs

### Auth Flow

- `AuthInterceptor` automatically attaches the JWT from `localStorage` to all outgoing requests
- `AuthGuard` protects all routes under `mainlayout`
- `jwt-decode` is used to read claims from the token on the client

### Charts & UI

- **ApexCharts** (`ng-apexcharts`) for all analytics/progress charts in `graficos/`
- **Angular Material** for form components and dialogs
- **Bootstrap 5** for layout and utility styling

---

## Domain Model (key aggregates)

```
Usuario
  └─ Rutina (mesocycle)
       └─ DiaRutina (training day)
            └─ EjercicioDiaRutina (exercise slot)
                  └─ Serie (set: reps + weight)

Sesion  ──references──► DiaRutina

RegistrosCorporal  ──belongs to──► Usuario
  (Pesos, Perímetros, Pliegues)

Ejercicio  ──belongs to──► SubGrupoMuscular ──belongs to──► GrupoMuscular
```

---

## Configuration Notes

- Backend secrets (JWT key, DB connection string, SMTP credentials) live in `appsettings.json` / user secrets — never commit real credentials
- The database connection currently points to a Neon PostgreSQL instance (`ep-proud-mud-abynjzcz-pooler.eu-west-2.aws.neon.tech`)
- API base URL in Angular is set via `src/environments/environment.ts`
