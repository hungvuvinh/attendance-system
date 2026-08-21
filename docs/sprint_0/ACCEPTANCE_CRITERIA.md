# Sprint 0 — Acceptance Criteria

> Each criterion maps to one or more requirements in [REQUIREMENTS.md](./REQUIREMENTS.md).  
> A criterion passes when its verification step produces the expected result.

---

## 1. Repository & Git Setup

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-GIT-01 | Git repository is initialized with at least one commit | `git log --oneline -1` returns a valid commit hash | REQ-GIT-01 |
| AC-GIT-02 | `.gitignore` correctly excludes build artifacts, `node_modules/`, `bin/`, `obj/`, `.env`, IDE files | Create a `.env` file and a `bin/` folder → `git status` does not show them as untracked | REQ-GIT-02 |
| AC-GIT-03 | `README.md` documents both setup modes (Docker Compose and standalone CLI) | Open `README.md` → it contains sections for Docker setup and local development setup with specific commands | REQ-GIT-03 |

---

## 2. Backend — Solution & Clean Architecture

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-BE-01 | Solution file `AttendanceSystem.sln` references all 7 projects | `dotnet sln backend/AttendanceSystem.sln list` returns 5 src + 2 test projects | REQ-BE-01 |
| AC-BE-02 | `dotnet build backend/AttendanceSystem.sln` succeeds with zero errors | Build exit code is 0, output contains "Build succeeded" | REQ-BE-01 |
| AC-BE-03 | Domain project has no project references (depends on nothing) | `Attendance.Domain.csproj` contains no `<ProjectReference>` elements | REQ-BE-03 |
| AC-BE-04 | Application project references only Domain | `Attendance.Application.csproj` has exactly one `<ProjectReference>` pointing to `Attendance.Domain` | REQ-BE-03 |
| AC-BE-05 | Infrastructure references Application (and transitively Domain) — not Api | `Attendance.Infrastructure.csproj` references `Attendance.Application` only | REQ-BE-03 |

---

## 3. Backend — Domain Entities & Enums

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-DOM-01 | `BaseEntity` exists with properties `Id` (Guid), `CreatedAt` (DateTime), `UpdatedAt` (DateTime) | Inspect `BaseEntity.cs` → all three properties present | REQ-DOM-01 |
| AC-DOM-02 | `RawAttendanceLog` extends `BaseEntity` with required fields | Inspect entity → contains `DeviceId`, `EmployeeDeviceId`, `Timestamp`, `Direction`, `ImportBatchId` | REQ-DOM-02 |
| AC-DOM-03 | `Employee` extends `BaseEntity` with required fields | Inspect entity → contains `EmployeeCode`, `FullName`, `DepartmentId`, `IsActive` | REQ-DOM-03 |
| AC-DOM-04 | `AttendanceDevice` extends `BaseEntity` with required fields | Inspect entity → contains `DeviceName`, `SerialNumber`, `Location`, `IsActive` | REQ-DOM-04 |
| AC-DOM-05 | `AttendanceStatus` enum contains all 13 defined statuses | Inspect enum → matches PROJECT_ATTENDANCE §5 list exactly | REQ-DOM-05 |

---

## 4. Backend — Application Layer

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-APP-01 | `IAppDbContext` interface exists and exposes `DbSet<T>` for each domain entity | Inspect interface → `DbSet<RawAttendanceLog>`, `DbSet<Employee>`, `DbSet<AttendanceDevice>` present | REQ-APP-01 |
| AC-APP-02 | `IAttendanceDeviceProvider` interface exists with at least one method signature | Inspect interface → contains method for reading device data | REQ-APP-02 |

---

## 5. Backend — Infrastructure Layer

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-INF-01 | `AppDbContext` implements `IAppDbContext` and inherits `DbContext` | Inspect class → both conditions met | REQ-INF-01 |
| AC-INF-02 | Entity configurations use `IEntityTypeConfiguration<T>` (not inline `OnModelCreating`) | At least one separate configuration class exists | REQ-INF-02 |
| AC-INF-03 | `MockAttendanceDeviceProvider` implements `IAttendanceDeviceProvider` | Inspect class → interface is implemented | REQ-INF-03 |
| AC-INF-04 | Connection string configures pooling and retry-on-failure | Inspect `Program.cs` or startup → `EnableRetryOnFailure` is called | REQ-INF-04 |

---

## 6. Backend — Worker Service

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-WRK-01 | Worker project exists and contains an `IHostedService` implementation | Inspect project → at least one class implements `IHostedService` or extends `BackgroundService` | REQ-WRK-01 |
| AC-WRK-02 | Worker project builds without errors | `dotnet build backend/src/Attendance.Worker/` exit code is 0 | REQ-WRK-02 |

---

## 7. Backend — API Host

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-API-01 | `GET /api/health` returns HTTP 200 with JSON `{ "status": "Healthy" }` | `curl http://localhost:5000/api/health` → 200 + expected JSON | REQ-API-02 |
| AC-API-02 | Swagger UI is accessible at `/swagger` | Navigate to `http://localhost:5000/swagger` → page loads with API documentation | REQ-API-01 |
| AC-API-03 | CORS allows `http://localhost:3000` and rejects other origins | Send request with `Origin: http://localhost:9999` → no `Access-Control-Allow-Origin` header in response | REQ-API-03 |
| AC-API-04 | Unhandled exceptions return generic error (no stack trace or sensitive info) | Trigger a 500 error → response body contains generic message, not exception details | REQ-API-04 |
| AC-API-05 | No secrets in committed config files | `git grep -i "password\|secret\|token"` in `appsettings.json` → no real values found | REQ-API-05 |

---

## 8. Backend Tests

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-TST-01 | Domain unit tests pass | `dotnet test backend/tests/Attendance.Domain.Tests/` → all tests pass | REQ-TST-01 |
| AC-TST-02 | API integration tests pass | `dotnet test backend/tests/Attendance.Api.IntegrationTests/` → health endpoint test passes | REQ-TST-02 |
| AC-TST-03 | Full test suite passes from solution level | `dotnet test backend/AttendanceSystem.sln` → 0 failures | REQ-TST-03 |

---

## 9. Frontend — Next.js Application

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-FE-01 | `npm install` completes without errors in `frontend/` | Exit code 0, `node_modules/` created | REQ-FE-01 |
| AC-FE-02 | `npm run build` completes without errors | Exit code 0, `.next/` build output created | REQ-FE-09 |
| AC-FE-03 | `npm run dev` starts dev server on `localhost:3000` | Server starts, `http://localhost:3000` returns HTTP 200 | REQ-FE-01 |
| AC-FE-04 | Dashboard page (`/`) renders with Sidebar and Header layout | Navigate to `/` → Sidebar and Header components are visible | REQ-FE-02, REQ-FE-03 |
| AC-FE-05 | Health page (`/health`) calls backend API and displays connection status | Navigate to `/health` → shows "Connected" or "Disconnected" based on backend availability | REQ-FE-04 |
| AC-FE-06 | Reusable UI components exist: Button, Card, Badge, Table | Files exist in `src/components/ui/` and export functional components | REQ-FE-05 |
| AC-FE-07 | Layout components exist: Navbar, Sidebar | Files exist in `src/components/layout/` and are used in `layout.tsx` | REQ-FE-06 |
| AC-FE-08 | API Client helper exists and is configured for backend base URL | `src/lib/api-client.ts` exports fetch functions targeting backend URL | REQ-FE-07 |

---

## 10. Database & Docker

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-DB-01 | `docker/postgres/init.sql` creates `attendance_db` database | Inspect SQL file → `CREATE DATABASE` or equivalent statement present | REQ-DB-01 |
| AC-DCK-01 | `docker-compose.yml` defines services: postgres, pgadmin, backend, frontend | Inspect file → all 4 services defined with correct images and ports | REQ-DCK-01 |
| AC-DCK-02 | `docker compose up` starts all services without errors | Run command → all containers reach "running" state | REQ-DCK-02 |
| AC-DCK-03 | Backend connects to PostgreSQL on startup (via Docker network) | Backend logs show successful DB connection, no connection refused errors | REQ-DCK-02 |

---

## 11. Data Integrity Foundation

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-DAT-01 | `RawAttendanceLog` and processed data entities are separate classes/tables | Inspect Domain → distinct entities for raw vs. processed data | REQ-DAT-01 |

---

## Definition of Done (Sprint 0)

All of the following must be true:

- [ ] All acceptance criteria above pass their verification steps
- [ ] `dotnet build backend/AttendanceSystem.sln` — zero errors
- [ ] `dotnet test backend/AttendanceSystem.sln` — zero failures
- [ ] `npm run build` in `frontend/` — zero errors
- [ ] `docker compose up` — all services start successfully
- [ ] Git repository has clean history with descriptive commit messages
- [ ] No secrets, passwords, or tokens in source control
- [ ] README.md documents setup for both Docker and standalone modes
