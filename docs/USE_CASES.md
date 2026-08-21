## Actor

### Nhân viên
Nhân viên là người trực tiếp thực hiện việc chấm công trên máy chấm công. Nhân viên có nhiều cách để thực hiện việc chấm công trên máy, có nhiều ca làm việc khác nhau và có các ngày được nghỉ lễ, nghỉ phép,... Nhân viên có thể truy cập hệ thống để kiểm tra trạng thái chấm công (đã qua xử lí) của bản thân.

### HR
HR là người quản lý nhân viên. HR có thể quản lý dữ liệu của nhân viên và quản lý các ca làm việc. HR có thể truy cập hệ thống để kiểm tra dữ liệu chấm công và được phép điều chỉnh dữ liệu nếu có lí do chính đáng. HR có thể xuất file excel dữ liệu chấm công của tháng.

### Hệ thống
Hệ thống tự động thu thập, xử lý, và tính toán dữ liệu chấm công từ các máy chấm công. Hệ thống thực hiện validation, mapping nhân viên, so khớp ca làm, phát hiện bất thường, và chuẩn bị bảng công sẵn sàng cho thanh toán lương.

---

## Use Cases

### **Nhân viên (Employee)**

#### UC-001: Chấm công trên máy chấm công
**Actor**: Nhân viên  
**Mô tả**: Nhân viên thực hiện chấm công bằng cách sử dụng một trong các phương thức: vân tay, khuôn mặt, thẻ hoặc mã nhân viên trên máy chấm công vật lý.

| Bước | Hoạt động |
|------|-----------|
| Precondition | Nhân viên được sắp xếp ca làm; máy chấm công hoạt động bình thường |
| 1 | Nhân viên tiếp cận máy chấm công |
| 2 | Nhân viên sử dụng một trong các phương thức: quét vân tay, khuôn mặt, thẻ, hoặc nhập mã |
| 3 | Máy chấm công xác thực nhân viên và ghi nhận thời gian chấm công |
| 4 | Máy chấm công hiển thị thông báo thành công |
| Postcondition | Raw attendance log được tạo (timestamp, device_id, employee_device_id, direction: check-in/check-out) |
| Exception | Nhân viên chưa được mapping → máy báo lỗi "Employee not found" |

#### UC-002: Xem bảng công cá nhân
**Actor**: Nhân viên  
**Mô tả**: Nhân viên truy cập hệ thống để xem bảng công đã được xử lý của mình (ngày/tháng).

| Bước | Hoạt động |
|------|-----------|
| Precondition | Nhân viên đã đăng nhập vào hệ thống |
| 1 | Nhân viên chọn menu "Bảng công cá nhân" hoặc "My Timesheet" |
| 2 | Hệ thống hiển thị bảng công của nhân viên theo tháng hiện tại |
| 3 | Nhân viên xem chi tiết: ngày, trạng thái (Present/Absent/Late/...), giờ vào/ra, ghi chú |
| 4 | Nhân viên có thể lọc theo tháng/năm |
| Postcondition | Nhân viên thấy bảng công đã được tính toán (Daily Attendance) |
| Exception | Bảng công chưa được xử lý → hiển thị "Processing..." |

---

### **HR / Manager**

#### UC-003: Quản lý nhân viên
**Actor**: HR  
**Mô tả**: HR quản lý thông tin nhân viên (thêm, sửa, xóa, kích hoạt/vô hiệu hóa).

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập với quyền HR/Admin |
| 1 | HR truy cập menu "Quản lý nhân viên" |
| 2 | HR xem danh sách nhân viên (Employee Code, Full Name, Department, Status) |
| **Thêm nhân viên** | |
| 3a | HR chọn "Thêm nhân viên mới" |
| 4a | HR nhập: mã nhân viên, tên đầy đủ, phòng ban, trạng thái |
| 5a | HR lưu → hệ thống tạo Employee record |
| **Sửa nhân viên** | |
| 3b | HR chọn nhân viên từ danh sách |
| 4b | HR chỉnh sửa thông tin (tên, phòng ban, v.v.) |
| 5b | HR lưu → hệ thống cập nhật record |
| **Vô hiệu hóa nhân viên** | |
| 3c | HR chọn nhân viên |
| 4c | HR chọn "Deactivate" → nhân viên không thể chấm công nữa |
| Postcondition | Employee record được tạo/cập nhật/vô hiệu hóa |
| Exception | Employee Code đã tồn tại → báo lỗi "Duplicate employee code" |

#### UC-004: Quản lý ca làm việc
**Actor**: HR  
**Mô tả**: HR định nghĩa, gán, và cập nhật ca làm việc cho nhân viên.

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập với quyền HR/Admin |
| 1 | HR truy cập menu "Quản lý ca làm" |
| **Định nghĩa ca làm** | |
| 2a | HR chọn "Tạo ca làm mới" |
| 3a | HR nhập: tên ca (Morning/Afternoon/Night), giờ bắt đầu, giờ kết thúc, mô tả |
| 4a | HR lưu → Shift record được tạo |
| **Gán ca làm cho nhân viên** | |
| 2b | HR chọn "Gán ca làm" |
| 3b | HR chọn nhân viên và ca làm |
| 4b | HR nhập ngày hiệu lực (từ - đến) |
| 5b | HR lưu → EmployeeShiftAssignment được tạo |
| Postcondition | Shift và EmployeeShiftAssignment được lưu; hệ thống sẽ so khớp raw logs với ca làm này |
| Exception | Nhân viên không tồn tại → báo lỗi |

#### UC-005: Xem dữ liệu chấm công
**Actor**: HR  
**Mô tả**: HR xem dữ liệu chấm công hàng ngày (raw logs + processed daily attendance) và phát hiện bất thường.

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập; dữ liệu đã được xử lý |
| 1 | HR truy cập "Bảng công" → chọn ngày/tháng/phòng ban |
| 2 | Hệ thống hiển thị Daily Attendance: nhân viên, trạng thái, giờ vào, giờ ra |
| 3 | HR có thể xem "Raw Logs" (dữ liệu thô từ máy chấm công) để audit |
| 4 | Hệ thống ghi dấu những nhân viên có bất thường (vàng = Late, đỏ = Absent, ...) |
| Postcondition | HR có cái nhìn đầy đủ về dữ liệu chấm công |

#### UC-006: Điều chỉnh dữ liệu chấm công
**Actor**: HR  
**Mô tả**: HR điều chỉnh dữ liệu chấm công khi có lý do chính đáng (ốm, công tác, v.v.). Mọi điều chỉnh phải có audit trail.

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập; bảng công chưa bị khóa (locked) |
| 1 | HR xem Daily Attendance của nhân viên cần điều chỉnh |
| 2 | HR chọn "Điều chỉnh" hoặc "Thêm adjustment" |
| 3 | HR chọn loại điều chỉnh: Đổi trạng thái (Present → Leave), Thêm giờ, Xóa bất thường, v.v. |
| 4 | HR nhập: ngày, giá trị mới, lý do (Reason), ghi chú |
| 5 | HR lưu → ManualAdjustment record được tạo với: changed_by, changed_at, old_value, new_value, reason |
| Postcondition | Daily Attendance được cập nhật; Raw Logs không bị sửa (bất biến); Adjustment record được lưu để audit |
| Exception | Bảng công đã khóa → báo lỗi "Cannot adjust locked timesheet" |

#### UC-007: Quản lý Leave/Holiday/Overtime
**Actor**: HR  
**Mô tả**: HR quản lý ngày lễ, leave, overtime của nhân viên.

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập |
| 1 | HR truy cập "Quản lý Leave/Holiday/Overtime" |
| **Thêm Holiday** | |
| 2a | HR chọn "Thêm ngày lễ" |
| 3a | HR nhập: ngày, tên ngày lễ (e.g., "Tết Nguyên Đán") |
| 4a | Hệ thống áp dụng tự động cho toàn công ty |
| **Phê duyệt Leave** | |
| 2b | HR xem danh sách leave pending từ nhân viên |
| 3b | HR chọn phê duyệt hoặc từ chối |
| 4b | Daily Attendance của nhân viên được cập nhật thành "LEAVE" |
| **Ghi nhận Overtime** | |
| 2c | HR nhập: nhân viên, ngày, số giờ, mô tả |
| 3c | Hệ thống lưu Overtime record; ảnh hưởng đến Monthly Timesheet (tính tiền thêm) |
| Postcondition | Holiday, Leave, Overtime được áp dụng; Daily Attendance và Monthly Timesheet được cập nhật tương ứng |

#### UC-008: Xuất bảng công tháng (Export Timesheet)
**Actor**: HR  
**Mô tả**: HR xuất bảng công tháng ra file Excel để gửi cho Payroll hoặc lưu trữ.

| Bước | Hoạt động |
|------|-----------|
| Precondition | HR đã đăng nhập; bảng công tháng đã hoàn tất xử lý và phê duyệt |
| 1 | HR truy cập "Export Timesheet" |
| 2 | HR chọn tháng/năm và phòng ban (hoặc toàn công ty) |
| 3 | HR chọn format export: "Excel", "CSV", hoặc "PDF" |
| 4 | Hệ thống tạo file chứa: Employee Code, Name, Department, ngày công, trạng thái từng ngày, tổng công, overtime, leave, v.v. |
| 5 | HR tải file về |
| Postcondition | File Excel được tạo và tải về; Monthly Timesheet có thể được gửi tới Payroll System |

---

### **Hệ thống (System/Automated Processes)**

#### UC-009: Thu thập dữ liệu từ máy chấm công
**Actor**: Device Connector (System)  
**Mô tả**: Hệ thống tự động kết nối với máy chấm công, lấy dữ liệu chấm công mới, và lưu vào Raw Attendance Logs.

| Bước | Hoạt động |
|------|-----------|
| Trigger | Theo lịch định kỳ (mỗi 5-15 phút) hoặc webhook từ máy chấm công |
| 1 | Device Connector gửi request tới máy chấm công (API hoặc SFTP) |
| 2 | Máy chấm công trả về raw attendance logs: {employee_device_id, timestamp, device_id, direction} |
| 3 | Hệ thống kiểm tra trùng lặp (nếu log này đã tồn tại → bỏ qua) |
| 4 | Hệ thống lưu vào Raw Attendance Logs table (immutable) |
| Postcondition | Raw Attendance Logs được thêm; ImportBatch record được tạo để tracking |
| Exception | Máy chấm công offline → log lỗi và retry sau; dữ liệu bị mất không được recover nếu máy không còn lưu |

#### UC-010: Xử lý & Tính toán dữ liệu chấm công
**Actor**: Attendance Processing Engine (System)  
**Mô tả**: Hệ thống xử lý raw logs, validation, mapping, shift matching, và tính công ngày/tháng.

| Bước | Hoạt động |
|------|-----------|
| Trigger | Theo lịch (mỗi đêm) hoặc manual từ HR |
| **Validation** | |
| 1a | Kiểm tra format, timestamp hợp lệ |
| 2a | Phát hiện trùng lặp (multiple taps cùng device trong < 30s) → chỉ giữ lần đầu |
| 3a | Kiểm tra timezone đúng |
| **Employee Mapping** | |
| 1b | Với mỗi raw log, tìm Employee dựa trên device_employee_id |
| 2b | Nếu không tìm thấy → tạo AttendanceException: "EMPLOYEE_NOT_MAPPED" |
| **Shift Matching** | |
| 1c | Tìm shift của nhân viên vào ngày đó |
| 2c | So khớp timestamp với shift time |
| 3c | Nếu check-in trước 07:00 nhưng shift bắt đầu 08:00 → ghi EARLY_CHECKIN |
| **Tính công ngày** | |
| 1d | Nếu có check-in + check-out, tính giờ công |
| 2d | So sánh với shift time để xác định status: PRESENT, LATE, EARLY_LEAVE, MISSING_CHECKIN, MISSING_CHECKOUT |
| 3d | Nếu ngày hôm đó là Holiday hoặc nhân viên có Leave → status = LEAVE hoặc HOLIDAY |
| Postcondition | Daily Attendance được tạo/cập nhật; AttendanceExceptions được ghi nhận |
| Exception | Shift không tồn tại → status = NO_SHIFT |

#### UC-011: Phát hiện bất thường (Exception Detection)
**Actor**: Attendance Processing Engine (System)  
**Mô tả**: Hệ thống tự động phát hiện và ghi nhận các bất thường để HR review.

| Bước | Hoạt động |
|------|-----------|
| Trigger | Sau mỗi lần tính công (UC-010) |
| 1 | Hệ thống scan Daily Attendance, phát hiện các status bất thường |
| 2 | Tạo AttendanceException records cho các case: |
|   | - MISSING_CHECKIN: check-out nhưng không có check-in |
|   | - MISSING_CHECKOUT: check-in nhưng không có check-out |
|   | - LATE: vào muộn hơn start shift time |
|   | - EARLY_LEAVE: ra sớm hơn end shift time |
|   | - DUPLICATE_LOG: máy chấm công ghi 2 lần liên tiếp |
|   | - EMPLOYEE_NOT_MAPPED: employee_device_id không tìm thấy employee |
|   | - NO_SHIFT: nhân viên không có shift được gán cho ngày đó |
|   | - DEVICE_OFFLINE: máy chấm công offline, không thu thập được dữ liệu |
| 3 | HR sẽ thấy danh sách exceptions trên dashboard, màu đỏ = cần review |
| Postcondition | AttendanceException records được lưu; HR có thể review và điều chỉnh nếu cần |

#### UC-012: Tính bảng công tháng (Monthly Timesheet Calculation)
**Actor**: Attendance Processing Engine (System)  
**Mô tả**: Hệ thống tổng hợp Daily Attendance thành Monthly Timesheet (tổng công, công chuẩn, overtime, leave, v.v.).

| Bước | Hoạt động |
|------|-----------|
| Trigger | Ngày cuối tháng hoặc ngày được HR chỉ định |
| 1 | Hệ thống tổng hợp từ Daily Attendance của nhân viên trong tháng |
| 2 | Tính toán: |
|   | - Total Working Days: số ngày phải làm |
|   | - Days Present: số ngày làm việc bình thường |
|   | - Days Absent: số ngày vắng |
|   | - Days Late: số ngày đi trễ |
|   | - Days Leave: số ngày nghỉ phép |
|   | - Days Holiday: số ngày lễ |
|   | - Overtime Hours: số giờ thêm |
|   | - Total Hours: tổng số giờ công |
| 3 | Áp dụng adjustment (UC-006) nếu HR đã điều chỉnh |
| 4 | Tạo MonthlyTimesheet record |
| Postcondition | Monthly Timesheet sẵn sàng để phê duyệt và export |
| Exception | Dữ liệu tháng chưa hoàn tất → báo lỗi "Incomplete data" |

#### UC-013: Khóa bảng công (Lock Timesheet)
**Actor**: HR / System  
**Mô tả**: Sau khi phê duyệt, HR khóa bảng công để ngăn điều chỉnh tiếp theo.

| Bước | Hoạt động |
|------|-----------|
| Precondition | Monthly Timesheet đã sẵn sàng, HR đã review |
| 1 | HR chọn "Phê duyệt & Khóa" hoặc "Approve & Lock" |
| 2 | Hệ thống thiết lập: monthly_timesheet.status = "LOCKED" |
| 3 | Monthly Timesheet có thể được export, không thể điều chỉnh |
| Postcondition | Bảng công được khóa; sẵn sàng cho thanh toán lương |
| Exception | Bảng công chưa phê duyệt → không thể khóa |
