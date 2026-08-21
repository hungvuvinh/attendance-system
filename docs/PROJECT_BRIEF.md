# Bài toán nghiệp vụ

Công ty có một hoặc nhiều máy chấm công.

```text
Nhân viên sử dụng:
- Vân tay
- Khuôn mặt
- Thẻ
- Mã nhân viên
để thực hiện chấm công.
```
```text
Máy chấm công sinh dữ liệu dạng:
EmployeeCode Timestamp Device Method
Ví dụ:
NV001 | 2026-08-10 07:52:15 | DEVICE01 | Fingerprint
NV001 | 2026-08-10 17:38:42 | DEVICE01 | Fingerprint
NV002 | 2026-08-10 08:14:03 | DEVICE01 | Face
NV002 | 2026-08-10 17:31:10 | DEVICE01 | Face
```
Dữ liệu gốc này chưa phải bảng chấm công.
Hệ thống phải biến dữ liệu:
Raw Attendance Logs
thành:
Daily Attendance
và sau đó:
Monthly Timesheet.

# Tính năng chính:

- Thu thập dữ liệu từ máy chấm công đa phương thức
- Lưu trữ raw attendance logs với tính toàn vẹn dữ liệu
- Xử lý tự động: validation, matching nhân viên, so khớp ca làm
- Tính công ngày/tháng và phát hiện bất thường (quên check-in/out, đi trễ, về sớm, v.v.)
- Cấp cho HR/PM điều chỉnh thủ công với audit trail đầy đủ
- Quản lý nhân viên, phòng ban, ca làm, leave, ngày lễ, overtime
- Export bảng công và tích hợp payroll

# Luồng tổng thể

Máy chấm công -> Device Connector -> Raw Attendance Logs -> Data Validation -> Employee Mapping
-> Shift Matching -> Attendance Processing Engine -> Daily Attendance -> Exception Detection -> HR
Review -> Monthly Timesheet -> Approval -> Excel / Payroll

# Nguyên tắc kiến trúc

```text
Phải tách: DỮ LIỆU GỐC và DỮ LIỆU ĐÃ TÍNH TOÁN.

Không được đọc dữ liệu máy chấm công rồi sửa trực tiếp dữ liệu gốc.

Bảng raw attendance phải có tính chất gần như: IMMUTABLE tức dữ liệu sau khi thu thập về không được tùy tiện sửa hoặc xóa.
Nếu thuật toán tính công thay đổi, hệ thống phải có khả năng:
Raw Logs -> Recalculate -> Daily Attendance mới
mà không cần đọc lại máy chấm công.
```

