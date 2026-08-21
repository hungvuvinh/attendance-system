# Sprint 0 Foundation Implementation Plan

> **For Antigravity:** REQUIRED WORKFLOW: Use `.agent/workflows/execute-plan.md` to execute this plan in single-flow mode.

**Goal:** Hoàn tất phần khởi tạo ứng dụng Sprint 0 với backend Clean Architecture, frontend Next.js, bộ kiểm thử tối thiểu và tạo database PostgreSQL rỗng.

**Architecture:** Giữ phân tách `backend/src` và `frontend/src`. Backend dùng hướng phụ thuộc `Domain <- Application <- Infrastructure <- Api`; Worker là host riêng. Sprint này chỉ scaffold boundary và health flow, chưa tạo bảng, DbContext, migration, mock device, processing engine, adjustment, timesheet calculation hoặc authentication.

**Tech Stack:** .NET Web API/Worker, C#, xUnit, ASP.NET integration testing, Next.js App Router, TypeScript, Tailwind CSS, PostgreSQL 16 chỉ dùng để tạo database rỗng.

---

## Scope and Decisions

### In scope

- Hoàn tất phần scaffold của REQ-GIT, REQ-BE, REQ-DOM, REQ-WRK, REQ-API, REQ-TST và REQ-FE.
- Tạo database PostgreSQL `attendance_db` rỗng, chưa tạo bảng hoặc migration.
- Có health endpoint, Swagger, CORS giới hạn cho frontend và global exception handling.

### Out of scope

- Authentication/authorization thực tế.
- DbContext, entity mapping, EF Core migration và mọi DDL tạo bảng.
- Mock device, device connector, scheduling sync và processing engine.
- Daily Attendance/Monthly Timesheet calculation.
- HR adjustment, audit workflow, leave/holiday/overtime, export và Docker.

### Assumptions requiring review before implementation

- Dùng .NET 8 LTS nếu máy dev chưa có version đã thống nhất; kiểm tra `dotnet --info` trước khi scaffold.
- PostgreSQL được chạy bằng công cụ sẵn có của môi trường hoặc dịch vụ PostgreSQL cục bộ; không thêm Docker file.
- Tạo database `attendance_db` một lần bằng quyền quản trị local; schema sẽ được thiết kế và tạo ở task database riêng sau này.
- PostgreSQL credentials chỉ nằm trong environment variables hoặc `.env.example`, không commit secret thật.

---

## Task 1: Establish repository baseline

**Files:**
- Create: `.gitignore` updates only if validation finds a missing pattern
- Create: `.env.example`
- Modify: `README.md`
- Modify: `docs/plans/task.md`

**Step 1: Verify local toolchain and repository state**

Run:

```powershell
dotnet --info
node --version
npm --version
git status --short --branch
```

Expected: required tools are available; if Git reports no repository, document that `git init` is required.

**Step 2: Add environment contract without secrets**

Document variables for PostgreSQL, backend URL and frontend API URL in `.env.example`. Use placeholders only.

**Step 3: Document standalone setup**

Add local CLI instructions to `README.md`, including required .NET/Node/PostgreSQL tools, ports, database creation, tests and the no-secret rule. Do not document Docker setup in this scope.

**Step 4: Update the table-only tracker**

Record this plan and task status in `docs/plans/task.md`; do not add prose outside the tracker table.

**Step 5: Verify baseline files**

Run:

```powershell
git grep -n -i -E 'password|secret|token|api[-_]?key' -- ':!docs/plans/*'
```

Expected: no real credentials or tokens are present.

---

## Task 2: Scaffold the backend solution and project references

**Files:**
- Create: `backend/AttendanceSystem.sln`
- Create: `backend/src/Attendance.Domain/Attendance.Domain.csproj`
- Create: `backend/src/Attendance.Application/Attendance.Application.csproj`
- Create: `backend/src/Attendance.Infrastructure/Attendance.Infrastructure.csproj`
- Create: `backend/src/Attendance.Worker/Attendance.Worker.csproj`
- Create: `backend/src/Attendance.Api/Attendance.Api.csproj`
- Create: `backend/tests/Attendance.Domain.Tests/Attendance.Domain.Tests.csproj`
- Create: `backend/tests/Attendance.Api.IntegrationTests/Attendance.Api.IntegrationTests.csproj`

**Step 1: Create projects with the selected target framework**

Use `dotnet new` commands rather than hand-editing generated SDK metadata.

**Step 2: Add only allowed project references**

- Application -> Domain
- Infrastructure -> Application
- Api -> Application and Infrastructure
- Worker -> Application and Infrastructure as required by hosting
- Domain.Tests -> Domain
- Api.IntegrationTests -> Api

Do not add references from Domain to any other project or from Infrastructure to Api.

**Step 3: Add required NuGet packages**

Use only packages needed for Swagger, xUnit and ASP.NET integration tests. Do not add EF Core/Npgsql packages until the database schema task is approved.

**Step 4: Verify project graph**

Run:

```powershell
dotnet sln backend/AttendanceSystem.sln list
dotnet build backend/AttendanceSystem.sln
```

Expected: seven projects listed and build succeeds, even before feature code is added.

---

## Task 3: Implement the domain foundation with tests first

**Files:**
- Create: `backend/src/Attendance.Domain/Entities/BaseEntity.cs`
- Create: `backend/src/Attendance.Domain/Entities/RawAttendanceLog.cs`
- Create: `backend/src/Attendance.Domain/Entities/Employee.cs`
- Create: `backend/src/Attendance.Domain/Entities/AttendanceDevice.cs`
- Create: `backend/src/Attendance.Domain/Entities/DailyAttendance.cs`
- Create: `backend/src/Attendance.Domain/Enums/AttendanceDirection.cs`
- Create: `backend/src/Attendance.Domain/Enums/AttendanceStatus.cs`
- Create: `backend/tests/Attendance.Domain.Tests/RawAttendanceLogTests.cs`
- Create: `backend/tests/Attendance.Domain.Tests/EntityValidationTests.cs`

**Step 1: Write failing tests**

Cover valid construction, required identifiers, immutable/raw-log behavior and the 13 status values from `PROJECT_ATTENDANCE.md`.

**Step 2: Run domain tests and confirm failure**

```powershell
dotnet test backend/tests/Attendance.Domain.Tests/Attendance.Domain.Tests.csproj
```

Expected: tests fail because the domain types are not implemented.

**Step 3: Implement minimal entities and enums**

Keep entities persistence-friendly, avoid infrastructure references, and do not add processing behavior that belongs to later sprints. Raw logs must not expose a mutation path intended for HR adjustment.

**Step 4: Run tests and inspect the diff**

Expected: domain tests pass; no project reference is added to Domain.

---

## Task 4: Add Application contracts

**Files:**
- Create: `backend/src/Attendance.Application/Interfaces/IAttendanceProcessingEngine.cs`

**Step 1: Define the deferred processing contract**

Define only the processing-engine boundary needed for future work, without implementing processing, device provider contracts or persistence contracts.

**Step 2: Implement interfaces**

Keep Application dependent only on Domain. Do not add `DbSet`, EF Core or device-provider implementation in this scope.

**Step 3: Verify references and build**

```powershell
dotnet build backend/src/Attendance.Application/Attendance.Application.csproj
```

Expected: build succeeds and the project contains only the Domain project reference.

---

## Task 5: Implement API host, health endpoint and error/security boundary

**Files:**
- Create: `backend/src/Attendance.Api/Program.cs`
- Create: `backend/src/Attendance.Api/Controllers/HealthController.cs`
- Create: `backend/src/Attendance.Api/Middleware/GlobalExceptionHandler.cs`
- Create: `backend/src/Attendance.Api/appsettings.json`
- Create: `backend/src/Attendance.Api/appsettings.Development.json`
- Create: `backend/tests/Attendance.Api.IntegrationTests/HealthEndpointTests.cs`

**Step 1: Write the failing integration test**

Use `WebApplicationFactory` to assert `GET /api/health` returns 200 and a JSON payload with `status = Healthy`.

**Step 2: Run the test and confirm failure**

```powershell
dotnet test backend/tests/Attendance.Api.IntegrationTests/Attendance.Api.IntegrationTests.csproj
```

Expected: failure because the API host/controller is not present.

**Step 3: Implement minimal API host**

Register controllers, Swagger in development, health endpoint and CORS restricted to `http://localhost:3000`. Do not register a DbContext or device provider in this scope.

**Step 4: Add generic exception handling**

Return a stable problem response without stack traces, connection strings or sensitive exception details. Log server-side without secrets.

**Step 5: Run integration tests**

Expected: health test passes and the test host does not require a production database connection.

---

## Task 6: Add Worker no-op host

**Files:**
- Create: `backend/src/Attendance.Worker/Program.cs`
- Create: `backend/src/Attendance.Worker/Services/AttendanceWorker.cs`

**Step 1: Implement a cancellation-aware no-op `BackgroundService`**

The worker should start, log a safe informational message and wait for cancellation. It must not sync real devices or mutate raw data in Sprint 0.

**Step 2: Build and run a short local smoke check**

```powershell
dotnet build backend/src/Attendance.Worker/Attendance.Worker.csproj
```

Expected: zero build errors.

---

## Task 7: Scaffold frontend and implement health-facing UI

**Files:**
- Modify: `frontend/package.json`
- Modify: `frontend/tsconfig.json`
- Modify: `frontend/tailwind.config.ts`
- Create: `frontend/src/app/layout.tsx`
- Create: `frontend/src/app/page.tsx`
- Create: `frontend/src/app/health/page.tsx`
- Create: `frontend/src/components/layout/Navbar.tsx`
- Create: `frontend/src/components/layout/Sidebar.tsx`
- Create: `frontend/src/components/ui/Button.tsx`
- Create: `frontend/src/components/ui/Card.tsx`
- Create: `frontend/src/components/ui/Badge.tsx`
- Create: `frontend/src/components/ui/Table.tsx`
- Create: `frontend/src/lib/api-client.ts`
- Create: `frontend/src/types/health.ts`

**Step 1: Define the API client contract and UI behavior**

Use an environment-backed backend URL and expose typed health responses; show Connected/Disconnected states and a retry action. Do not hardcode secrets or scatter API URLs.

**Step 2: Implement layout and reusable components**

Keep components accessible, responsive and compatible with `prefers-reduced-motion`. Use existing brand/design documentation if added later; do not introduce unapproved brand colors.

**Step 3: Add dashboard and `/health` pages**

The dashboard is Sprint 0 shell content only. The health page must handle timeout, non-2xx response and unavailable backend without throwing an unhandled client error.

**Step 4: Install and build**

```powershell
Set-Location frontend
npm install
npm run build
```

Expected: install and production build succeed.

---

## Task 8: Create the empty PostgreSQL database

**Files:**
- Create: `.env.example` if it does not already exist
- Do not modify: `docker-compose.yml`, `docker/postgres/init.sql`, `backend/Dockerfile`, `frontend/Dockerfile`

**Step 1: Document the local database contract**

Record the database name, host, port and placeholder credentials in `.env.example` and `README.md`. Do not commit a real password.

**Step 2: Create the database only**

Run the PostgreSQL administration command appropriate to the local installation, for example:

```powershell
createdb -h $env:POSTGRES_HOST -p $env:POSTGRES_PORT -U $env:POSTGRES_ADMIN_USER attendance_db
```

Expected: `attendance_db` is created successfully and contains no application tables or EF migration history.

**Step 3: Verify the database is empty**

Use `psql` to list relations and confirm no application schema has been created. Do not run `dotnet ef database update`.

---

## Task 9: Run the complete verification matrix

**Files:**
- Modify: `docs/sprint_0/ACCEPTANCE_CRITERIA.md` only if a verified criterion needs clarification
- Modify: `docs/plans/task.md`

**Step 1: Verify backend build and tests**

```powershell
dotnet build backend/AttendanceSystem.sln
dotnet test backend/AttendanceSystem.sln --no-build
```

Expected: zero errors and zero failed tests.

**Step 2: Verify frontend**

```powershell
Set-Location frontend
npm run build
```

Expected: successful production build.

**Step 3: Verify security and architecture invariants**

Check project references, CORS origin, no secrets, no database table/migration creation, no mock-device implementation and generic error responses.

**Step 4: Verify acceptance criteria**

Map each AC ID to command output or an explicit manual check. Record failures instead of weakening tests.

**Step 5: Update tracker and report**

Update `docs/plans/task.md` and provide the required summary: Changed, Tests Run, Known Risks, Migration, Security Impact, Follow-up.

---

## Dependencies and Gates

- Task 1 must complete before committing environment/configuration changes.
- Task 2 must complete before domain/application/infrastructure implementation.
- Task 5 can proceed with an isolated test host and does not require a database connection.
- Task 7 can proceed in parallel with backend Tasks 3–6 after the API response contract is fixed.
- Task 8 must run after local PostgreSQL access is confirmed and before final verification.
- Task 9 is the final gate; no Sprint 1 implementation starts while the reduced Sprint 0 criteria remain unverified.

## Security and Data Rules

- No production connections, data deletion or schema migration.
- No hardcoded password, token, API key or secret.
- No application tables are created in this scope; raw attendance persistence belongs to a later database task.
- CORS is restricted to the approved local frontend origin.
- Error responses do not expose stack traces or infrastructure details.

## Completion Criteria

- All applicable Sprint 0 acceptance criteria pass.
- Backend solution builds and tests pass.
- Frontend installs and builds.
- `attendance_db` exists as an empty local database.
- No Docker file is modified and no migration is created.
- Git history contains focused commits and the working tree is clean.

## Handoff

Plan complete and saved to `docs/plans/2026-08-20-sprint-0-foundation.md`.

Next step: review the assumptions and run `.agent/workflows/execute-plan.md` to execute this plan task-by-task in single-flow mode. Do not implement before intern/mentor approval.
