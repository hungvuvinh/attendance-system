# [Sprint 0] Khởi tạo môi trường & Kiến trúc dự án Attendance System (Phương án 2: `backend/` & `frontend/`)

Khởi tạo toàn bộ cấu trúc dự án **Hệ thống Thu thập và Tổng hợp Chấm công (Project A)** theo đúng chuẩn của chương trình **VissSoft AI Engineering Internship 2026**, phân tách rõ ràng hệ sinh thái `backend/` (.NET Web API + Clean Architecture) và `frontend/` (Next.js + TypeScript), CSDL (PostgreSQL) và Docker Compose.

---

## 1. Requirement Understanding (Hiểu yêu cầu)
- **Mục tiêu**: Xây dựng nền tảng vững chắc cho Sprint 0, đáp ứng trọn vẹn yêu cầu kiến trúc, kiểm thử, phân tầng nghiệp vụ và phân tách rõ ràng giữa Frontend và Backend.
- **Phạm vi Sprint 0**:
  1. Tạo thư mục gốc `d:/Intern/attendance-system/`.
  2. Xây dựng phân hệ `backend/` theo mô hình **Clean Architecture**:
     - `Attendance.Domain`: Entities, Value Objects, Enums, Domain Exceptions.
     - `Attendance.Application`: Interfaces (`IAppDbContext`, `IAttendanceDeviceProvider`, `IAttendanceProcessingEngine`), DTOs, Service Contracts.
     - `Attendance.Infrastructure`: PostgreSQL DbContext (EF Core), Configurations, Mock Device Provider implementation.
     - `Attendance.Worker`: Background service (IHostedService) chuẩn bị cho Device Sync & Processing Queue.
     - `Attendance.Api`: REST API, Controllers (`HealthController`), Swagger UI, Global Error Handling.
     - `Attendance.Domain.Tests` & `Attendance.Api.IntegrationTests`: Bộ test tự động.
  3. Xây dựng phân hệ `frontend/` bằng **Next.js (App Router, TypeScript, Tailwind CSS)**:
     - Layout chuẩn doanh nghiệp, Sidebar/Navbar điều hướng.
     - Trang Dashboard và trang chẩn đoán kết nối (`/health`).
     - Tích hợp API Client kết nối mượt mà tới Backend.
  4. Cấu hình CSDL PostgreSQL & Docker: `docker-compose.yml`, `docker/postgres/init.sql`.
  5. Cấu hình Git, `.gitignore`, `AGENTS.md` tuân thủ quy tắc VissSoft, và `README.md` hướng dẫn chạy.

---

## 2. Relevant Code & Existing Structure (Mã nguồn liên quan)
- Tham chiếu: [`Tài liệu thực tập dự án chấm công.pdf`](file:///d:/Intern/visssoft-internship/Tài%20liệu%20thực%20tập%20dự%20án%20chấm%20công.pdf), [`VissSoft AI Engineering Internship 2026.pdf`](file:///d:/Intern/visssoft-internship/VissSoft%20AI%20Engineering%20Internship%202026.pdf), [`docs/PROJECT_ATTENDANCE.md`](file:///d:/Intern/visssoft-internship/docs/PROJECT_ATTENDANCE.md).
- Toàn bộ source code mới sẽ được khởi tạo trong `d:/Intern/attendance-system/`.

---

## 3. Proposed Approach & Architecture (Giải pháp đề xuất)

```text
d:/Intern/attendance-system/
├── backend/
│   ├── src/
│   │   ├── Attendance.Domain/               # Core Entities (RawLog, Employee, Shift, etc.)
│   │   ├── Attendance.Application/          # Interfaces, DTOs, Abstractions
│   │   ├── Attendance.Infrastructure/       # EF Core, PostgreSQL, Mock Providers
│   │   ├── Attendance.Worker/               # Background Services (Sync & Calculation)
│   │   └── Attendance.Api/                  # Web API Host, Swagger, Controllers
│   ├── tests/
│   │   ├── Attendance.Domain.Tests/         # Unit Tests
│   │   └── Attendance.Api.IntegrationTests/ # Integration Tests
│   ├── AttendanceSystem.sln                 # Solution File .NET
│   └── Dockerfile                           # Multi-stage Dockerfile cho Backend
│
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── layout.tsx                   # Main Layout (Sidebar + Header)
│   │   │   ├── page.tsx                     # Dashboard tổng quan
│   │   │   └── health/page.tsx              # Diagnostic & API Health Check
│   │   ├── components/
│   │   │   ├── ui/                          # Button, Card, Badge, Table
│   │   │   └── layout/                      # Navbar, Sidebar
│   │   ├── lib/                             # API Client fetch helper
│   │   └── types/                           # TypeScript interfaces
│   ├── package.json
│   ├── tsconfig.json
│   ├── tailwind.config.ts
│   └── Dockerfile                           # Dockerfile cho Next.js
│
├── docker/
│   └── postgres/
│       └── init.sql                         # Khởi tạo DB & Extensions ban đầu
├── docker-compose.yml                       # PostgreSQL 16, pgAdmin 4, Backend, Frontend
├── .gitignore
├── README.md
└── AGENTS.md
```

---

## 4. Files Affected (Danh sách file sẽ tạo)

### Backend (.NET Solution & Projects)
- `[NEW]` [backend/AttendanceSystem.sln](file:///d:/Intern/attendance-system/backend/AttendanceSystem.sln)
- `[NEW]` [backend/src/Attendance.Domain/Attendance.Domain.csproj](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Attendance.Domain.csproj)
- `[NEW]` [backend/src/Attendance.Domain/Entities/BaseEntity.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Entities/BaseEntity.cs)
- `[NEW]` [backend/src/Attendance.Domain/Entities/RawAttendanceLog.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Entities/RawAttendanceLog.cs)
- `[NEW]` [backend/src/Attendance.Domain/Entities/Employee.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Entities/Employee.cs)
- `[NEW]` [backend/src/Attendance.Domain/Entities/AttendanceDevice.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Entities/AttendanceDevice.cs)
- `[NEW]` [backend/src/Attendance.Domain/Enums/AttendanceStatus.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Domain/Enums/AttendanceStatus.cs)
- `[NEW]` [backend/src/Attendance.Application/Attendance.Application.csproj](file:///d:/Intern/attendance-system/backend/src/Attendance.Application/Attendance.Application.csproj)
- `[NEW]` [backend/src/Attendance.Application/Interfaces/IAppDbContext.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Application/Interfaces/IAppDbContext.cs)
- `[NEW]` [backend/src/Attendance.Application/Interfaces/IAttendanceDeviceProvider.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Application/Interfaces/IAttendanceDeviceProvider.cs)
- `[NEW]` [backend/src/Attendance.Infrastructure/Attendance.Infrastructure.csproj](file:///d:/Intern/attendance-system/backend/src/Attendance.Infrastructure/Attendance.Infrastructure.csproj)
- `[NEW]` [backend/src/Attendance.Infrastructure/Persistence/AppDbContext.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Infrastructure/Persistence/AppDbContext.cs)
- `[NEW]` [backend/src/Attendance.Infrastructure/Providers/MockAttendanceDeviceProvider.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Infrastructure/Providers/MockAttendanceDeviceProvider.cs)
- `[NEW]` [backend/src/Attendance.Api/Attendance.Api.csproj](file:///d:/Intern/attendance-system/backend/src/Attendance.Api/Attendance.Api.csproj)
- `[NEW]` [backend/src/Attendance.Api/Program.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Api/Program.cs)
- `[NEW]` [backend/src/Attendance.Api/Controllers/HealthController.cs](file:///d:/Intern/attendance-system/backend/src/Attendance.Api/Controllers/HealthController.cs)
- `[NEW]` [backend/src/Attendance.Api/appsettings.json](file:///d:/Intern/attendance-system/backend/src/Attendance.Api/appsettings.json)
- `[NEW]` [backend/src/Attendance.Api/appsettings.Development.json](file:///d:/Intern/attendance-system/backend/src/Attendance.Api/appsettings.Development.json)
- `[NEW]` [backend/tests/Attendance.Domain.Tests/Attendance.Domain.Tests.csproj](file:///d:/Intern/attendance-system/backend/tests/Attendance.Domain.Tests/Attendance.Domain.Tests.csproj)
- `[NEW]` [backend/tests/Attendance.Domain.Tests/RawAttendanceLogTests.cs](file:///d:/Intern/attendance-system/backend/tests/Attendance.Domain.Tests/RawAttendanceLogTests.cs)
- `[NEW]` [backend/tests/Attendance.Api.IntegrationTests/Attendance.Api.IntegrationTests.csproj](file:///d:/Intern/attendance-system/backend/tests/Attendance.Api.IntegrationTests/Attendance.Api.IntegrationTests.csproj)
- `[NEW]` [backend/tests/Attendance.Api.IntegrationTests/HealthEndpointTests.cs](file:///d:/Intern/attendance-system/backend/tests/Attendance.Api.IntegrationTests/HealthEndpointTests.cs)

### Frontend (Next.js Application)
- `[NEW]` [frontend/package.json](file:///d:/Intern/attendance-system/frontend/package.json)
- `[NEW]` [frontend/tsconfig.json](file:///d:/Intern/attendance-system/frontend/tsconfig.json)
- `[NEW]` [frontend/tailwind.config.js](file:///d:/Intern/attendance-system/frontend/tailwind.config.js)
- `[NEW]` [frontend/src/app/layout.tsx](file:///d:/Intern/attendance-system/frontend/src/app/layout.tsx)
- `[NEW]` [frontend/src/app/page.tsx](file:///d:/Intern/attendance-system/frontend/src/app/page.tsx)
- `[NEW]` [frontend/src/app/health/page.tsx](file:///d:/Intern/attendance-system/frontend/src/app/health/page.tsx)
- `[NEW]` [frontend/src/lib/api-client.ts](file:///d:/Intern/attendance-system/frontend/src/lib/api-client.ts)

### Root Configs & Docker
- `[NEW]` [docker-compose.yml](file:///d:/Intern/attendance-system/docker-compose.yml)
- `[NEW]` [.gitignore](file:///d:/Intern/attendance-system/.gitignore)
- `[NEW]` [README.md](file:///d:/Intern/attendance-system/README.md)
- `[NEW]` [AGENTS.md](file:///d:/Intern/attendance-system/AGENTS.md)

---

## 5. Database Impact (Ảnh hưởng CSDL)
- Tạo database `attendance_db` trên PostgreSQL 16.
- Cấu hình EF Core Npgsql connection string với cơ chế pooling và retry on failure.
- Chuẩn bị sẵn kiến trúc Entity Mapping tách biệt (`IEntityTypeConfiguration<T>`).

---

## 6. Security Impact (Bảo mật)
- Cấu hình CORS chặt chẽ: chỉ cho phép origin của frontend (`http://localhost:3000`).
- Không lưu secret vào git (sử dụng `.env.example` và `appsettings.Development.json`).
- Cấu hình Global Exception Handling Middleware tránh leak thông tin nhạy cảm.

---

## 7. Edge Cases (Trường hợp biên)
- Hỗ trợ cả 2 chế độ:
  - Chạy toàn bộ qua Docker Compose (1 lệnh `docker compose up`).
  - Chạy trực tiếp qua CLI (.NET SDK + Node.js) khi chưa bật Docker daemon.

---

## 8. Test Plan (Kịch bản kiểm thử)
1. **Domain Unit Tests**: Chạy `dotnet test backend/tests/Attendance.Domain.Tests` kiểm tra tính hợp lệ của Domain Entities.
2. **API Integration Tests**: Chạy `dotnet test backend/tests/Attendance.Api.IntegrationTests` kiểm tra endpoint `GET /api/health` trả về HTTP 200 và JSON payload `"status": "Healthy"`.
3. **Frontend Build & Render**: Chạy `npm run build` và kiểm tra trang Dashboard + Health Page kết nối thành công.

---

## 9. Regression Risks
- Không có (dự án khởi tạo mới).

---

## 10. Open Questions
- Toàn bộ thiết kế đã được thống nhất theo **Phương án 2 (`backend/` và `frontend/`)**.
