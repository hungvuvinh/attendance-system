# Sprint 1 — Requirements: Employee, Device & Mock Device

> Derived from [PROJECT_ATTENDANCE.md](../PROJECT_ATTENDANCE.md), [USE_CASES.md](../USE_CASES.md), and [SDLC_AI_SKILLS.md](../SDLC_AI_SKILLS.md).  
> **Phạm vi Sprint 1**: Tập trung vào Module Employee + Module Attendance Device + Module Device Vendor + Mock Device Connector (Chưa triển khai Raw Attendance Log và Device Employee Mapping vào CSDL ở sprint này).

---

## 1. Module 01 — Employee Management (Quản lý Nhân viên)

| ID | Requirement | Description | Source |
|----|-------------|-------------|--------|
| REQ-EMP-01 | Employee Entity | Entity extending `BaseEntity` với đầy đủ các thuộc tính: <br>• **Mã nhân viên** (`EmployeeCode`, unique, indexed) <br>• **Họ tên** (`FullName`) <br>• **Phòng ban** (`DepartmentId`, FK) <br>• **Chức vụ** (`Position`) <br>• **Ngày vào làm** (`HireDate`, Date) <br>• **Ngày nghỉ việc** (`ResignDate`, Date nullable) <br>• **Trạng thái** (`IsActive`, bool) <br>• **Mã nhân viên trên máy chấm công** (`AttendanceCode` / `DeviceEnrollmentCode`, indexed) | Module 01 Spec |
| REQ-EMP-02 | Department Master Data | Entity `Department` (`Id`, `Code` unique, `Name`, `Description`) phục vụ danh mục phòng ban và endpoint lookup `GET /api/departments` | Master Data |
| REQ-EMP-03 | Employee Validation Rules | • `EmployeeCode` bắt buộc, không trùng lặp <br>• `FullName` bắt buộc <br>• `DepartmentId` phải tồn tại <br>• `AttendanceCode` (mã trên máy chấm công) định dạng hợp lệ <br>• `ResignDate` (nếu có) phải $\ge$ `HireDate` | AGENTS.md §6 |
| REQ-EMP-04 | Employee Service & DTOs | Interface `IEmployeeService` trong `Attendance.Application` xử lý phân trang, lọc theo phòng ban/từ khóa, DTOs: `EmployeeDto`, `CreateEmployeeRequest`, `UpdateEmployeeRequest` | Clean Architecture |
| REQ-EMP-05 | Employee REST API | Controller `EmployeesController` với các endpoint: <br>• `GET /api/employees` (search, department, paging) <br>• `GET /api/employees/{id}` <br>• `POST /api/employees` <br>• `PUT /api/employees/{id}` <br>• `DELETE /api/employees/{id}` (soft-delete / deactivate) | UC-003 |

---

## 2. Module 02 — Attendance Device Management (Quản lý Máy chấm công & Nhà cung cấp)

| ID | Requirement | Description | Source |
|----|-------------|-------------|--------|
| REQ-DEV-01 | Device Vendor Entity | Entity `DeviceVendor` quản lý hãng/nhà cung cấp thiết bị: <br>• `Id` (Guid) <br>• `Code` (`MOCK`, `ZKTECO`, `HIKVISION`, `RONALDJACK`, unique) <br>• `Name` (Tên hãng) <br>• `Description` <br>• `IsActive` (bool) | Module 02 Spec |
| REQ-DEV-02 | Attendance Device Entity | Entity `AttendanceDevice` với đầy đủ thông tin: <br>• `Id` (Device ID) <br>• `DeviceName` (Tên máy) <br>• `VendorId` (FK -> DeviceVendor) <br>• `Model` (Model máy) <br>• `SerialNumber` (Serial Number, unique, indexed) <br>• `IpAddress` (IP) <br>• `Port` (Port, default 4370) <br>• `Location` (Địa điểm đặt máy) <br>• `Status` (Active/Inactive) <br>• `ConnectionStatus` (Online/Offline/Unknown) <br>• `LastSyncAt` (Thời điểm đồng bộ gần nhất) | Module 02 Spec |
| REQ-DEV-03 | Multi-Vendor Extensibility | Kiến trúc Adapter / Factory Pattern: Thiết kế hệ thống mở rộng hỗ trợ nhiều hãng máy khác nhau trong tương lai, **không phụ thuộc cứng (hardcode) vào một hãng máy cụ thể**. | Module 02 Spec |
| REQ-DEV-04 | Device REST API | Controller `DevicesController` và `DeviceVendorsController`: <br>• `GET /api/device-vendors` <br>• `GET /api/devices` <br>• `GET /api/devices/{id}` <br>• `POST /api/devices` <br>• `PUT /api/devices/{id}` <br>• `DELETE /api/devices/{id}` <br>• `POST /api/devices/{id}/test-connection` | UC-004 |

---

## 3. Module 03 — Device Connector & Mock Device (Adapter mô phỏng)

| ID | Requirement | Description | Source |
|----|-------------|-------------|--------|
| REQ-CON-01 | Connector Interface | `IDeviceConnector` trong `Attendance.Application` định nghĩa các hành vi kết nối thiết bị: <br>• `CheckConnectionAsync(AttendanceDevice device)` (kiểm tra Online/Offline) <br>• `ReadEmployeesAsync(AttendanceDevice device)` (đọc danh sách NV trên máy) <br>• `ReadAttendanceLogsAsync(AttendanceDevice device, DateTime? since)` (đọc log) | Module 03 Spec |
| REQ-CON-02 | Connector Factory | `IDeviceConnectorFactory` phân giải connector tương ứng theo `Vendor.Code` của từng thiết bị. | Clean Architecture |
| REQ-CON-03 | Mock Device Implementation | `MockDeviceConnector` trong `Attendance.Infrastructure` mô phỏng thiết bị chấm công: giả lập kiểm tra online/offline, đọc danh sách nhân viên mock, tạo dữ liệu attendance log giả lập với độ trễ mạng thực tế. | Module 03 Spec |
| REQ-CON-04 | Test Connection API | Endpoint `POST /api/devices/{id}/test-connection` kích hoạt connector kiểm tra trạng thái và cập nhật `ConnectionStatus` của máy. | UC-004 |

---

## 4. Database & Persistence (EF Core & PostgreSQL)

| ID | Requirement | Description | Source |
|----|-------------|-------------|--------|
| REQ-DB-01 | PostgreSQL Schema | Tạo bảng: `departments`, `employees`, `device_vendors`, `attendance_devices` | AGENTS.md §5 |
| REQ-DB-02 | IAppDbContext | Interface trong `Attendance.Application` quản lý `DbSet<Department>`, `DbSet<Employee>`, `DbSet<DeviceVendor>`, `DbSet<AttendanceDevice>` | Clean Architecture |
| REQ-DB-03 | Fluent API Configurations | Cấu hình Index, Unique Key (`EmployeeCode`, `SerialNumber`, `VendorCode`), Foreign Key (`DepartmentId`, `VendorId`) | Clean Architecture |
| REQ-DB-04 | EF Core Migration | Migration `Initial_Sprint1_Employee_Device_Vendor` tạo cấu trúc CSDL chuẩn | AGENTS.md §5 |
| REQ-DB-05 | Seed Initial Data | Tự động seed dữ liệu mẫu cho `departments`, `device_vendors` (Mock, ZKTeco, Hikvision), và 1 máy chấm công mẫu khi khởi chạy app | Dev Experience |

---

## 5. Frontend UI (Next.js 15)

| ID | Requirement | Description | Source |
|----|-------------|-------------|--------|
| REQ-FE-01 | Employees Page (`/employees`) | Bảng danh sách nhân viên hiển thị: Mã NV, Họ tên, Phòng ban, Chức vụ, Ngày vào làm, Mã trên máy chấm công, Trạng thái (Active/Inactive) + Form Thêm/Sửa nhân viên | Module 01 |
| REQ-FE-02 | Devices Page (`/devices`) | Bảng máy chấm công: Tên máy, Hãng/Nhà cung cấp, Model, Serial, IP/Port, Địa điểm, Trạng thái hoạt động, Trạng thái kết nối (Online/Offline badge) + Nút "Test Connection" | Module 02 |
| REQ-FE-03 | API Client | Cập nhật API client fetch methods trong `frontend/src/lib/api-client.ts` | Architecture |
