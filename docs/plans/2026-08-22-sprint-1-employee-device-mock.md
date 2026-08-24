# Sprint 1: Employee, Device & Mock Device Implementation Plan

> **For Antigravity:** REQUIRED WORKFLOW: Use `.agent/workflows/execute-plan.md` to execute this plan in single-flow mode.

**Goal:** Implement Employee Management (with Position, HireDate, ResignDate, AttendanceCode), Device Vendor & Attendance Device Management (with Model, IP/Port, Serial, Connection Status), Multi-Vendor Connector Architecture (Adapter + Factory) with Mock Device Connector, PostgreSQL persistence via EF Core, REST APIs, and Next.js frontend pages.

**Architecture:** Clean Architecture with Domain entities in `Attendance.Domain`, Service contracts, DTOs, and Connector abstractions in `Attendance.Application`, EF Core DbContext, Configurations, Migrations, and `MockDeviceConnector` in `Attendance.Infrastructure`, REST Controllers in `Attendance.Api`, and Next.js 15 App Router pages in `frontend/`.

**Tech Stack:** .NET 10 (C#), Entity Framework Core, PostgreSQL 16 (Npgsql), xUnit, Next.js 15, React 19, TypeScript, Tailwind CSS.

---

## Proposed Changes & Task Breakdown

### Task 1: Domain Entities & Enums (Department, Employee, DeviceVendor, AttendanceDevice)

**Files:**
- Create: `backend/src/Attendance.Domain/Entities/Department.cs`
- Create: `backend/src/Attendance.Domain/Entities/DeviceVendor.cs`
- Modify: `backend/src/Attendance.Domain/Entities/Employee.cs`
- Modify: `backend/src/Attendance.Domain/Entities/AttendanceDevice.cs`
- Create: `backend/src/Attendance.Domain/Enums/DeviceConnectionStatus.cs`
- Test: `backend/tests/Attendance.Domain.Tests/MasterDataEntityTests.cs`

**Step 1: Write failing unit tests for Domain Entities**
Test creation, required fields, unique invariants, and validations for `Employee`, `AttendanceDevice`, `Department`, and `DeviceVendor`.

**Step 2: Run test to verify it fails**
Run: `dotnet test backend/tests/Attendance.Domain.Tests/ --filter FullyQualifiedName~MasterDataEntityTests`
Expected: FAIL.

**Step 3: Implement Domain Entities**
- `Department`: `Code`, `Name`, `Description`.
- `Employee`: `EmployeeCode`, `FullName`, `DepartmentId`, `Position`, `HireDate`, `ResignDate`, `AttendanceCode`, `IsActive`.
- `DeviceVendor`: `Code`, `Name`, `Description`, `IsActive`.
- `AttendanceDevice`: `DeviceName`, `VendorId`, `Model`, `SerialNumber`, `IpAddress`, `Port`, `Location`, `IsActive`, `ConnectionStatus`, `LastSyncAt`.

**Step 4: Run test to verify it passes**
Run: `dotnet test backend/tests/Attendance.Domain.Tests/`
Expected: PASS.

---

### Task 2: Infrastructure Layer — EF Core PostgreSQL, DbContext & Configurations

**Files:**
- Modify: `backend/src/Attendance.Infrastructure/Attendance.Infrastructure.csproj` (Add `Npgsql.EntityFrameworkCore.PostgreSQL`, `Microsoft.EntityFrameworkCore.Design`)
- Create: `backend/src/Attendance.Application/Interfaces/IAppDbContext.cs`
- Create: `backend/src/Attendance.Infrastructure/Persistence/AppDbContext.cs`
- Create: `backend/src/Attendance.Infrastructure/Persistence/Configurations/DepartmentConfiguration.cs`
- Create: `backend/src/Attendance.Infrastructure/Persistence/Configurations/EmployeeConfiguration.cs`
- Create: `backend/src/Attendance.Infrastructure/Persistence/Configurations/DeviceVendorConfiguration.cs`
- Create: `backend/src/Attendance.Infrastructure/Persistence/Configurations/AttendanceDeviceConfiguration.cs`
- Create: `backend/src/Attendance.Infrastructure/DependencyInjection.cs`
- Modify: `docker/postgres/init.sql`
- Modify: `docker-compose.yml`

**Step 1: Add EF Core NuGet packages and define IAppDbContext**
Add PostgreSQL dependencies to `Attendance.Infrastructure.csproj`. Define `IAppDbContext` interface exposing DbSets for `Departments`, `Employees`, `DeviceVendors`, and `AttendanceDevices`.

**Step 2: Implement Fluent API Entity Configurations**
Configure primary keys, foreign keys (`DepartmentId`, `VendorId`), unique indexes (`employee_code`, `serial_number`, `vendor_code`), and column lengths.

**Step 3: Implement AppDbContext and Dependency Injection**
Implement `AppDbContext` and `AddInfrastructure` registration with PostgreSQL connection string and retry policy.

**Step 4: Update Docker PostgreSQL setup**
Update `docker-compose.yml` and `docker/postgres/init.sql` for PostgreSQL 16.

**Step 5: Verify build**
Run: `dotnet build backend/src/Attendance.Infrastructure/`
Expected: Build Succeeded.

---

### Task 3: Multi-Vendor Connector Architecture & Mock Device Connector

**Files:**
- Create: `backend/src/Attendance.Application/DTOs/DeviceConnectionResultDto.cs`
- Create: `backend/src/Attendance.Application/DTOs/DeviceEmployeeInfoDto.cs`
- Create: `backend/src/Attendance.Application/DTOs/DeviceRawLogDto.cs`
- Create: `backend/src/Attendance.Application/Interfaces/IDeviceConnector.cs`
- Create: `backend/src/Attendance.Application/Interfaces/IDeviceConnectorFactory.cs`
- Create: `backend/src/Attendance.Infrastructure/Connectors/MockDeviceConnector.cs`
- Create: `backend/src/Attendance.Infrastructure/Connectors/DeviceConnectorFactory.cs`
- Test: `backend/tests/Attendance.Domain.Tests/DeviceConnectorTests.cs`

**Step 1: Write failing unit test for Connector Factory & Mock Connector**
Test that `IDeviceConnectorFactory` correctly resolves `MockDeviceConnector` for vendor `MOCK`, and verify mock connection checking and synthetic log generation.

**Step 2: Run test to verify it fails**
Run: `dotnet test backend/tests/Attendance.Domain.Tests/ --filter FullyQualifiedName~DeviceConnectorTests`
Expected: FAIL.

**Step 3: Implement MockDeviceConnector and Factory**
Implement `IDeviceConnector` for Mock device simulating network latency, online/offline status, and dummy employee/log reads. Implement `DeviceConnectorFactory` to resolve connectors by vendor code.

**Step 4: Run test to verify it passes**
Run: `dotnet test backend/tests/Attendance.Domain.Tests/ --filter FullyQualifiedName~DeviceConnectorTests`
Expected: PASS.

---

### Task 4: Application Services (EmployeeService & DeviceService)

**Files:**
- Create: `backend/src/Attendance.Application/DTOs/EmployeeDtos.cs`
- Create: `backend/src/Attendance.Application/DTOs/DepartmentDtos.cs`
- Create: `backend/src/Attendance.Application/DTOs/DeviceDtos.cs`
- Create: `backend/src/Attendance.Application/DTOs/DeviceVendorDtos.cs`
- Create: `backend/src/Attendance.Application/DTOs/PagedResult.cs`
- Create: `backend/src/Attendance.Application/Interfaces/IEmployeeService.cs`
- Create: `backend/src/Attendance.Application/Interfaces/IDeviceService.cs`
- Create: `backend/src/Attendance.Application/Services/EmployeeService.cs`
- Create: `backend/src/Attendance.Application/Services/DeviceService.cs`
- Create: `backend/src/Attendance.Application/DependencyInjection.cs`

**Step 1: Define DTOs & Service Interfaces**
Request/Response DTOs with validation rules for Employee, Department, Device, and Vendor.

**Step 2: Implement EmployeeService & DeviceService**
Implement business logic: uniqueness validation, department/vendor validation, soft-delete, search, and device connection testing via Connector Factory.

**Step 3: Register Application dependencies**
Add `AddApplication` extension method.

**Step 4: Verify build**
Run: `dotnet build backend/src/Attendance.Application/`
Expected: Build Succeeded.

---

### Task 5: Web API Controllers & Database Seeding

**Files:**
- Create: `backend/src/Attendance.Api/Controllers/DepartmentsController.cs`
- Create: `backend/src/Attendance.Api/Controllers/EmployeesController.cs`
- Create: `backend/src/Attendance.Api/Controllers/DeviceVendorsController.cs`
- Create: `backend/src/Attendance.Api/Controllers/DevicesController.cs`
- Modify: `backend/src/Attendance.Api/Program.cs`
- Modify: `backend/src/Attendance.Api/appsettings.json`
- Modify: `backend/src/Attendance.Api/appsettings.Development.json`
- Test: `backend/tests/Attendance.Api.IntegrationTests/EmployeeEndpointTests.cs`
- Test: `backend/tests/Attendance.Api.IntegrationTests/DeviceEndpointTests.cs`

**Step 1: Write integration tests for API endpoints**
Write tests for `DepartmentsController`, `EmployeesController`, `DevicesController`, `DeviceVendorsController`, and `POST /api/devices/{id}/test-connection`.

**Step 2: Implement Controllers & configure Program.cs**
Implement all 4 controllers with Swagger annotations, register DB and services in DI, seed default departments and vendors (Mock, ZKTeco, Hikvision) on startup.

**Step 3: Run integration tests**
Run: `dotnet test backend/tests/Attendance.Api.IntegrationTests/`
Expected: PASS.

---

### Task 6: Frontend Pages (Employees & Devices Management)

**Files:**
- Modify: `frontend/src/lib/api-client.ts`
- Create: `frontend/src/types/employee.ts`
- Create: `frontend/src/types/device.ts`
- Create: `frontend/src/app/employees/page.tsx`
- Create: `frontend/src/app/devices/page.tsx`
- Create: `frontend/src/components/employees/EmployeeModal.tsx`
- Create: `frontend/src/components/devices/DeviceModal.tsx`

**Step 1: Define TypeScript interfaces and API Client helpers**
Add typed API functions for departments, employees, device vendors, and devices.

**Step 2: Build Employee Management Page (`/employees`)**
Table with columns: Mã NV, Họ tên, Phòng ban, Chức vụ, Ngày vào làm, Mã máy chấm công, Trạng thái (Active/Inactive), Actions. Modal for adding and editing employee with full validation.

**Step 3: Build Device Management Page (`/devices`)**
Table with columns: Tên máy, Nhà cung cấp (Vendor), Model, Serial Number, IP:Port, Địa điểm, Trạng thái, Trạng thái kết nối. Nút "Test Connection" với phản hồi trực quan thời gian thực. Modal thêm/sửa máy chấm công.

**Step 4: Verify Frontend Build**
Run: `npm run build` in `frontend/`
Expected: Build Succeeded.

---

### Task 7: Full Verification & Quality Gates

**Step 1: Run complete backend test suite**
Run: `dotnet test backend/AttendanceSystem.sln`
Expected: All tests pass with 0 failures.

**Step 2: Verify Frontend production build**
Run: `npm run build` in `frontend/`
Expected: Zero build errors.

**Step 3: Update docs and task tracker**
Update `docs/plans/task.md`.
