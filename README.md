# Attendance System

## Current status

The repository contains the Sprint 0 foundation and the completed Sprint 1 Task 1-2 implementation.

Implemented:

- .NET Clean Architecture solution and domain entities
- Next.js frontend shell with API health diagnostics
- EF Core PostgreSQL persistence contracts, DbContext and entity configurations
- PostgreSQL 16 Docker Compose service

Sprint 1 Task 3-7 are still pending: device connectors, application services, REST APIs and seed data, management pages, and migration/full verification.

## Local setup

Prerequisites:

- .NET 10 SDK and runtime
- Node.js 20+ and npm
- Docker with Docker Compose, or PostgreSQL 16 with `createdb` and `psql` available on `PATH`

Copy `.env.example` to a local environment file and replace placeholders locally. Do not commit passwords, tokens or other credentials.

### PostgreSQL with Docker Compose

Set the local password in the current PowerShell session, then start PostgreSQL:

```powershell
$env:POSTGRES_PASSWORD = "replace-locally"
docker compose up -d postgres
```

The Compose service uses PostgreSQL 16, creates `attendance_db`, and exposes it on port `5432` by default. Do not commit the password or any other credentials.

### Local PostgreSQL installation

Create the database as a PostgreSQL administrator if PostgreSQL is already installed locally:

```powershell
createdb -h localhost -p 5432 -U postgres attendance_db
```

The EF Core model and configurations are available, but the initial migration and application startup registration are scheduled for later Sprint 1 tasks.

Build and test the backend:

```powershell
dotnet build backend/AttendanceSystem.sln
dotnet test backend/AttendanceSystem.sln
```

Install and build the frontend:

```powershell
Set-Location frontend
npm install
npm run build
```

The API health endpoint is `GET /api/health`; the frontend diagnostic page is `/health`.

## Deferred work

Initial EF Core migration, API startup registration and seed data, mock devices, device synchronization, attendance processing, and management UI are planned for later tasks.
