# Sprint 1 — Acceptance Criteria

> Each criterion maps directly to requirements in [REQUIREMENTS.md](./REQUIREMENTS.md).  
> A criterion passes when its verification step produces the exact expected result.

---

## 1. Database & Persistence (EF Core & PostgreSQL)

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-DB-01 | EF Core Migration creates tables `departments`, `employees`, `device_vendors`, `attendance_devices` | Run migrations → PostgreSQL contains exactly these 4 tables with correct columns and data types | REQ-DB-01, REQ-DB-04 |
| AC-DB-02 | Unique indexes exist on `employees.employee_code`, `device_vendors.code`, and `attendance_devices.serial_number` | Database schema inspection confirms unique index constraints | REQ-EMP-03, REQ-DEV-01, REQ-DEV-02 |
| AC-DB-03 | Foreign Key `employees.department_id` references `departments.id` | Database schema confirms FK constraint | REQ-EMP-01 |
| AC-DB-04 | Foreign Key `attendance_devices.vendor_id` references `device_vendors.id` | Database schema confirms FK constraint | REQ-DEV-02 |
| AC-DB-05 | Database auto-seeds initial departments and device vendors on startup | Query `departments` and `device_vendors` → seeded data is returned | REQ-DB-05 |

---

## 2. Module 01 — Employee Management (Backend)

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-EMP-01 | `GET /api/departments` returns list of departments | Send GET request → HTTP 200 with list of active departments | REQ-EMP-02 |
| AC-EMP-02 | `GET /api/employees` returns paginated list of employees with search and department filtering | Send `GET /api/employees?search=...&departmentId=...&page=1&pageSize=10` → HTTP 200 with items and total count | REQ-EMP-04, REQ-EMP-05 |
| AC-EMP-03 | `GET /api/employees/{id}` returns complete employee details including Position, HireDate, ResignDate, AttendanceCode | Send GET with valid ID → HTTP 200 with all required fields | REQ-EMP-01, REQ-EMP-05 |
| AC-EMP-04 | `POST /api/employees` creates a new employee with all specified fields | Send POST payload with `employeeCode`, `fullName`, `departmentId`, `position`, `hireDate`, `attendanceCode` → HTTP 201 Created | REQ-EMP-01, REQ-EMP-05 |
| AC-EMP-05 | `POST /api/employees` rejects duplicate `EmployeeCode` | Send POST with existing employee code → HTTP 400/409 error | REQ-EMP-03 |
| AC-EMP-06 | `PUT /api/employees/{id}` updates employee fields including Position, ResignDate, AttendanceCode | Send PUT request → HTTP 200 with updated fields reflected | REQ-EMP-05 |
| AC-EMP-07 | `DELETE /api/employees/{id}` deactivates employee (`IsActive` becomes false) | Send DELETE request → HTTP 204 No Content | REQ-EMP-05 |

---

## 3. Module 02 & 03 — Attendance Device Management & Connector (Backend)

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-DEV-01 | `GET /api/device-vendors` returns list of supported device vendors (e.g. MOCK, ZKTECO, HIKVISION) | Send GET request → HTTP 200 with list of vendors | REQ-DEV-01, REQ-DEV-04 |
| AC-DEV-02 | `GET /api/devices` returns list of devices including Vendor, Model, IP/Port, Location, Status, ConnectionStatus | Send GET request → HTTP 200 with list of devices | REQ-DEV-02, REQ-DEV-04 |
| AC-DEV-03 | `POST /api/devices` creates a new device with Model, SerialNumber, IP, Port, Location, VendorId | Send valid POST payload → HTTP 201 Created | REQ-DEV-02, REQ-DEV-04 |
| AC-DEV-04 | `POST /api/devices` rejects duplicate `SerialNumber` | Send POST with existing serial number → HTTP 400/409 error | REQ-DEV-02 |
| AC-DEV-05 | `PUT /api/devices/{id}` updates device settings | Send PUT request → HTTP 200 with updated device info | REQ-DEV-04 |
| AC-DEV-06 | `POST /api/devices/{id}/test-connection` tests device connectivity via Mock Connector | Send POST request → HTTP 200 with `{ "connected": true, "responseTimeMs": ..., "connectionStatus": "Online" }` | REQ-CON-04 |
| AC-DEV-07 | Multi-Vendor Architecture: Connector Factory resolves connector based on Vendor Code | Unit test confirms `IDeviceConnectorFactory` returns `MockDeviceConnector` for `MOCK` vendor and throws/not-supported for unimplemented vendors without hardcoding vendor logic in core services | REQ-DEV-03, REQ-CON-02 |

---

## 4. Frontend UI (Next.js 15)

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-FE-01 | Employees page (`/employees`) renders table with: Mã NV, Họ tên, Phòng ban, Chức vụ, Ngày vào làm, Mã máy chấm công, Trạng thái | Navigate to `/employees` → table shows all requested columns | REQ-FE-01 |
| AC-FE-02 | Employee Add/Edit modal includes inputs for: Mã NV, Họ tên, Phòng ban, Chức vụ, Ngày vào làm, Ngày nghỉ việc, Mã máy chấm công | Open modal → all fields are available and can be submitted | REQ-FE-01 |
| AC-FE-03 | Devices page (`/devices`) renders table with: Tên máy, Nhà cung cấp, Model, Serial Number, IP:Port, Địa điểm, Trạng thái, Trạng thái kết nối | Navigate to `/devices` → table displays all device attributes | REQ-FE-02 |
| AC-FE-04 | "Test Connection" button on Devices page triggers connectivity check and displays real-time Online/Offline feedback | Click "Test Connection" on a device row → button shows loading state, then updates status badge with latency | REQ-FE-02 |
| AC-FE-05 | Frontend builds cleanly with zero errors | `npm run build` exits with code 0 in `frontend/` | Architecture |

---

## 5. Automated Testing Suite

| ID | Criterion | Verification | Req |
|----|-----------|-------------|-----|
| AC-TST-01 | Domain unit tests pass | `dotnet test backend/tests/Attendance.Domain.Tests/` → 0 failures | Quality Gate |
| AC-TST-02 | Application Service & Mock Connector unit tests pass | `dotnet test backend/tests/Attendance.Domain.Tests/` → 0 failures | Quality Gate |
| AC-TST-03 | API Integration tests pass for Employees, Devices, and Vendors | `dotnet test backend/tests/Attendance.Api.IntegrationTests/` → 0 failures | Quality Gate |
| AC-TST-04 | Solution build succeeds | `dotnet build backend/AttendanceSystem.sln` → 0 errors | Quality Gate |

---

## Definition of Done (Sprint 1)

- [ ] Tất cả tiêu chí từ AC-DB-01 đến AC-TST-04 đều đạt kết quả mong muốn
- [ ] Schema CSDL quản lý chuẩn qua EF Core Migrations cho 4 bảng (`departments`, `employees`, `device_vendors`, `attendance_devices`)
- [ ] Nhân viên có đầy đủ: Mã NV, Họ tên, Phòng ban, Chức vụ, Ngày vào làm, Ngày nghỉ việc, Trạng thái, Mã NV trên máy chấm công
- [ ] Máy chấm công có đầy đủ: Device ID, Tên máy, IP, Port, Model, Serial Number, Địa điểm, Trạng thái, Last Sync, Connection Status, Nhà cung cấp (Device Vendor)
- [ ] Kiến trúc Multi-Vendor Connector (Adapter + Factory) phân tách độc lập, không hardcode hãng máy
- [ ] Mock Device Connector mô phỏng kiểm tra kết nối online/offline và đọc dữ liệu giả lập
- [ ] Giao diện Next.js cho `/employees` và `/devices` hoàn chỉnh, responsive, kết nối API mượt mà
- [ ] Toàn bộ test tự động pass (`dotnet test backend/AttendanceSystem.sln`) và frontend build pass (`npm run build`)
