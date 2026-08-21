# Project A — Hệ thống Thu thập và Tổng hợp Chấm công

## 1. Mục tiêu

Xây dựng hệ thống đọc dữ liệu máy chấm công, lưu raw log, tính công ngày/tháng, phát hiện bất thường, cho phép HR điều chỉnh có audit và export dữ liệu.

## 2. Luồng chính

```text
Attendance Device
  -> Device Connector
  -> Raw Attendance Logs
  -> Validation
  -> Employee Mapping
  -> Shift Matching
  -> Attendance Processing Engine
  -> Daily Attendance
  -> Exceptions
  -> HR Review / Adjustment
  -> Monthly Timesheet
  -> Approval / Lock
  -> Export
```

## 3. Module

- Employee
- Department
- Attendance Device
- Device Employee Mapping
- Mock Device
- Device Connector
- Import Batch
- Raw Attendance Logs
- Shift
- Employee Shift Assignment
- Attendance Processing Engine
- Attendance Exception
- Manual Adjustment
- Leave / Holiday
- Overtime
- Daily Attendance
- Monthly Timesheet
- Authentication
- Authorization
- Audit Log
- Monitoring

## 4. Quy tắc dữ liệu quan trọng

Raw Attendance Data phải tách khỏi Processed Attendance Data.

Không sửa raw log khi HR điều chỉnh.

Mọi adjustment phải lưu:

```text
ChangedBy
ChangedAt
OldValue
NewValue
Reason
```

## 5. Các trạng thái tối thiểu

```text
PRESENT
ABSENT
LATE
EARLY_LEAVE
LATE_AND_EARLY
MISSING_CHECKIN
MISSING_CHECKOUT
NO_SHIFT
LEAVE
HOLIDAY
BUSINESS_TRIP
REMOTE_WORK
OVERTIME
```

## 6. Edge cases bắt buộc

- Duplicate log
- Missing check-in
- Missing check-out
- Multiple taps
- Overnight shift 22:00–06:00
- Holiday
- Leave
- Device offline
- Wrong timezone
- Employee not mapped
- Timesheet locked

## 7. Stack khuyến nghị

```text
Frontend: Next.js
Backend: .NET Web API
Database: PostgreSQL
Container: Docker / Docker Compose
Source: GitHub
```

## 8. Sprint gợi ý

### Sprint 0
Onboarding, Git, environment, PR.

### Sprint 1
Employee + Device + Mock Device.

### Sprint 2
Device Sync + Raw Logs + Import Batch.

### Sprint 3
Shift + Assignment.

### Sprint 4
Attendance Processing Engine.

### Sprint 5
Exceptions + Adjustment + Audit.

### Sprint 6
Monthly Timesheet + Export.

### Sprint 7
Authentication + Authorization.

### Sprint 8
Docker + Logging + Monitoring + Backup + Load Test.

## 9. Demo cuối kỳ

```text
Mock Device
  -> Sync
  -> Raw Log
  -> Calculate
  -> Exception
  -> Adjustment
  -> Recalculate
  -> Monthly Timesheet
  -> Approve
  -> Lock
  -> Export
  -> Audit
```
