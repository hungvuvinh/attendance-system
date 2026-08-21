# Attendance System

## Sprint 0

This repository contains the initial backend and frontend foundation for the attendance system.

Current scope:

- .NET Clean Architecture solution and domain foundation
- Next.js frontend shell with API health diagnostics
- PostgreSQL database creation only; no application tables or migrations yet

## Local setup

Prerequisites:

- .NET 8 SDK or compatible newer SDK with the .NET 8 runtime installed
- Node.js 20+ and npm
- PostgreSQL 16, with `createdb` and `psql` available on `PATH`

Copy `.env.example` to a local environment file and replace placeholders locally. Do not commit passwords, tokens or other credentials.

Create the empty database as a PostgreSQL administrator:

```powershell
createdb -h localhost -p 5432 -U postgres attendance_db
```

This Sprint intentionally creates no application tables and no EF Core migration.

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

Database schema, EF Core persistence, mock devices, Docker files, device synchronization and attendance processing are planned for later tasks.
