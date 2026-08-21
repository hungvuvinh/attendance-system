# Sprint 0 — Requirements

> Derived from [sprint_0.md](./sprint_0.md) and [PROJECT_ATTENDANCE.md](../PROJECT_ATTENDANCE.md).

---

## 1. Repository & Git

| ID | Requirement | Source |
|----|------------|--------|
| REQ-GIT-01 | Initialize a Git repository in `d:/Intern/attendance-system/` | sprint_0 §1 |
| REQ-GIT-02 | Provide a `.gitignore` covering .NET, Node.js, Docker, OS, IDE artifacts | sprint_0 §4 |
| REQ-GIT-03 | Provide a `README.md` with setup instructions for both Docker Compose and standalone (CLI) modes | sprint_0 §4, §7 |
| REQ-GIT-04 | Include `AGENTS.md` with VissSoft AI Engineering coding rules | sprint_0 §1 |

---

## 2. Backend — .NET Web API (Clean Architecture)

### 2.1 Solution Structure

| ID | Requirement | Source |
|----|------------|--------|
| REQ-BE-01 | Create a .NET Solution (`AttendanceSystem.sln`) with 5 source projects and 2 test projects | sprint_0 §3 |
| REQ-BE-02 | Follow Clean Architecture: Domain → Application → Infrastructure → Api layering | sprint_0 §3 |
| REQ-BE-03 | No project may reference a layer above it (Domain has zero project references; Application references only Domain; etc.) | Clean Architecture principle |

### 2.2 Attendance.Domain

| ID | Requirement | Source |
|----|------------|--------|
| REQ-DOM-01 | Define a `BaseEntity` with `Id` (Guid), `CreatedAt`, `UpdatedAt` | sprint_0 §4 |
| REQ-DOM-02 | Define `RawAttendanceLog` entity with: `DeviceId`, `EmployeeDeviceId`, `Timestamp`, `Direction` (CheckIn/CheckOut), `ImportBatchId` | PROJECT_ATTENDANCE §2-4 |
| REQ-DOM-03 | Define `Employee` entity with: `EmployeeCode`, `FullName`, `DepartmentId`, `IsActive` | PROJECT_ATTENDANCE §3 |
| REQ-DOM-04 | Define `AttendanceDevice` entity with: `DeviceName`, `SerialNumber`, `Location`, `IsActive` | PROJECT_ATTENDANCE §3 |
| REQ-DOM-05 | Define `AttendanceStatus` enum with all 13 statuses from PROJECT_ATTENDANCE §5 | PROJECT_ATTENDANCE §5 |

### 2.3 Attendance.Application

| ID | Requirement | Source |
|----|------------|--------|
| REQ-APP-01 | Define `IAppDbContext` interface exposing `DbSet<T>` for all domain entities | sprint_0 §4 |
| REQ-APP-02 | Define `IAttendanceDeviceProvider` interface for device data retrieval | sprint_0 §4 |
| REQ-APP-03 | Application project references only Attendance.Domain | Clean Architecture |

### 2.4 Attendance.Infrastructure

| ID | Requirement | Source |
|----|------------|--------|
| REQ-INF-01 | Implement `AppDbContext` using EF Core with Npgsql (PostgreSQL) provider | sprint_0 §3, §5 |
| REQ-INF-02 | Use `IEntityTypeConfiguration<T>` for entity mapping (separate configuration classes) | sprint_0 §5 |
| REQ-INF-03 | Implement `MockAttendanceDeviceProvider` as a stub for `IAttendanceDeviceProvider` | sprint_0 §4 |
| REQ-INF-04 | Configure connection string with pooling and retry-on-failure | sprint_0 §5 |

### 2.5 Attendance.Worker

| ID | Requirement | Source |
|----|------------|--------|
| REQ-WRK-01 | Create a Worker project with `IHostedService` scaffolding for future Device Sync & Processing Queue | sprint_0 §3 |
| REQ-WRK-02 | Worker must build and start without errors (no-op in Sprint 0) | sprint_0 §1 |

### 2.6 Attendance.Api

| ID | Requirement | Source |
|----|------------|--------|
| REQ-API-01 | Create ASP.NET Web API host with Swagger/OpenAPI UI | sprint_0 §4 |
| REQ-API-02 | Implement `GET /api/health` endpoint returning `{ "status": "Healthy", ... }` with HTTP 200 | sprint_0 §8 |
| REQ-API-03 | Configure CORS: allow only `http://localhost:3000` (frontend origin) | sprint_0 §6 |
| REQ-API-04 | Implement Global Exception Handling Middleware (no sensitive data leakage) | sprint_0 §6 |
| REQ-API-05 | Use `appsettings.json` and `appsettings.Development.json` — no secrets committed to git | sprint_0 §6 |

### 2.7 Backend Dockerfile

| ID | Requirement | Source |
|----|------------|--------|
| REQ-BDF-01 | Multi-stage Dockerfile for backend (.NET) | sprint_0 §3 |

---

## 3. Backend Tests

| ID | Requirement | Source |
|----|------------|--------|
| REQ-TST-01 | `Attendance.Domain.Tests`: Unit tests verifying domain entity validity (e.g., `RawAttendanceLog` construction) | sprint_0 §8 |
| REQ-TST-02 | `Attendance.Api.IntegrationTests`: Integration test verifying `GET /api/health` returns HTTP 200 + `"status": "Healthy"` | sprint_0 §8 |
| REQ-TST-03 | All tests must pass via `dotnet test` | sprint_0 §8 |

---

## 4. Frontend — Next.js (App Router, TypeScript, Tailwind CSS)

| ID | Requirement | Source |
|----|------------|--------|
| REQ-FE-01 | Initialize Next.js project with App Router, TypeScript, Tailwind CSS | sprint_0 §1 |
| REQ-FE-02 | Create main `layout.tsx` with enterprise-style Sidebar + Header navigation | sprint_0 §4 |
| REQ-FE-03 | Create Dashboard page (`/`) with overview layout | sprint_0 §4 |
| REQ-FE-04 | Create Health Check page (`/health`) that calls backend `GET /api/health` and displays connection status | sprint_0 §4 |
| REQ-FE-05 | Create reusable UI components: Button, Card, Badge, Table in `components/ui/` | sprint_0 §3 |
| REQ-FE-06 | Create layout components: Navbar, Sidebar in `components/layout/` | sprint_0 §3 |
| REQ-FE-07 | Create API Client fetch helper in `lib/api-client.ts` | sprint_0 §4 |
| REQ-FE-08 | Define TypeScript interfaces in `types/` directory | sprint_0 §3 |
| REQ-FE-09 | Frontend must build successfully via `npm run build` | sprint_0 §8 |
| REQ-FE-10 | Frontend Dockerfile for containerized deployment | sprint_0 §3 |

---

## 5. Database & Docker

| ID | Requirement | Source |
|----|------------|--------|
| REQ-DB-01 | Create `docker/postgres/init.sql` to initialize `attendance_db` database and required extensions | sprint_0 §4, §5 |
| REQ-DCK-01 | Create `docker-compose.yml` with services: PostgreSQL 16, pgAdmin 4, Backend, Frontend | sprint_0 §4 |
| REQ-DCK-02 | Full stack must start with single `docker compose up` command | sprint_0 §7 |
| REQ-DCK-03 | Support standalone mode (no Docker) using .NET SDK + Node.js directly | sprint_0 §7 |

---

## 6. Data Rules (Sprint 0 Foundation)

| ID | Requirement | Source |
|----|------------|--------|
| REQ-DAT-01 | Raw Attendance Data must be separated from Processed Attendance Data at the entity/table level | PROJECT_ATTENDANCE §4 |
| REQ-DAT-02 | Raw logs must never be modified directly — adjustments create separate audit records | PROJECT_ATTENDANCE §4 |

---

## 7. Non-Functional Requirements

| ID | Requirement | Source |
|----|------------|--------|
| REQ-NFR-01 | No secrets, passwords, tokens, or API keys committed to source control | AGENTS.md §6 |
| REQ-NFR-02 | All database schema changes must use EF Core migrations | AGENTS.md §5 |
| REQ-NFR-03 | Provide `.env.example` for environment variable documentation | sprint_0 §6 |
