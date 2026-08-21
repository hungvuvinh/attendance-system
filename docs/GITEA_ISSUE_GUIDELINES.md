# 📋 Quy Định Tạo Issue Trên Gitea

> Tài liệu hướng dẫn chuẩn hóa cách tạo issue cho **tất cả Developer** trong team.

---

## 1. Quy Tắc Chung

| # | Quy tắc                                                                     | Bắt buộc          |
| - | ---------------------------------------------------------------------------- | ------------------- |
| 1 | Mỗi issue chỉ giải quyết**1 vấn đề duy nhất**                  | ✅                  |
| 2 | Tiêu đề phải**rõ ràng, cụ thể**, bắt đầu bằng `[Module]` | ✅                  |
| 3 | Mô tả phải có**Acceptance Criteria** (tiêu chí nghiệm thu)      | ✅                  |
| 4 | Gắn**Label** phù hợp                                                | ✅                  |
| 5 | Gắn**Assignee** (người thực hiện)                                 | ✅                  |
| 6 | Set**Milestone** nếu có sprint đang chạy                           | ⚠️ Khuyến khích |
| 7 | Không tạo issue trùng lặp —**tìm trước khi tạo**              | ✅                  |

---

## 2. Định Dạng Tiêu Đề (Title)

```
[Module] Hành-động + Đối-tượng
```

### Ví dụ đúng ✅

```
[Auth] Implement JWT refresh token
[API] Add pagination to GET /api/users
[UI] Fix responsive layout on mobile dashboard
[Database] Add migration for user_preferences table
[DevOps] Configure CI/CD pipeline for staging
```

### Ví dụ sai ❌

```
Fix bug              ← Quá chung chung
Update code          ← Không rõ update gì
Lỗi                  ← Không có context
Làm tính năng mới    ← Không cụ thể
```

---

## 3. Mẫu Issue — Feature (Tính năng mới)

```markdown
## Mô tả
Mô tả ngắn gọn tính năng cần xây dựng, mục đích và lý do.

## Chi tiết yêu cầu
- [ ] Yêu cầu 1: ...
- [ ] Yêu cầu 2: ...
- [ ] Yêu cầu 3: ...

## Acceptance Criteria (Tiêu chí nghiệm thu)
- [ ] User có thể ...
- [ ] API trả về đúng format ...
- [ ] Unit test pass 100%
- [ ] Không ảnh hưởng đến tính năng hiện tại

## Technical Notes
- Stack/Framework: ...
- File cần sửa: ...
- Dependency: (phụ thuộc issue nào, nếu có)

## Mockup / References
(Đính kèm ảnh hoặc link nếu có)
```

**Labels:** `enhancement`, `backend` / `frontend` / `fullstack`

---

## 4. Mẫu Issue — Bug (Lỗi)

```markdown
## Mô tả lỗi
Mô tả ngắn gọn lỗi gặp phải.

## Cách tái hiện (Steps to Reproduce)
1. Bước 1: ...
2. Bước 2: ...
3. Bước 3: ...

## Kết quả thực tế (Actual Result)
- Hiện tại: ...

## Kết quả mong đợi (Expected Result)
- Mong muốn: ...

## Môi trường (Environment)
- Browser/OS: ...
- Endpoint/URL: ...
- Account test: ...

## Screenshot / Log
(Đính kèm ảnh chụp màn hình hoặc log lỗi)

## Mức độ nghiêm trọng
- [ ] P0 — Hệ thống không hoạt động
- [ ] P1 — Ảnh hưởng nhiều user
- [ ] P2 — Ảnh hưởng ít user, có workaround
- [ ] P3 — Cosmetic / UI nhỏ
```

**Labels:** `bug`, `priority:P0` / `priority:P1` / `priority:P2` / `priority:P3`

---

## 5. Mẫu Issue — Task (Công việc kỹ thuật)

```markdown
## Mô tả
Mô tả công việc cần thực hiện (refactor, setup, config, documentation...).

## Checklist
- [ ] Task 1: ...
- [ ] Task 2: ...
- [ ] Task 3: ...

## Acceptance Criteria
- [ ] Hoàn thành tất cả checklist
- [ ] Code review approved
- [ ] Không break existing tests

## Notes
(Ghi chú thêm nếu cần)
```

**Labels:** `task`, `devops` / `documentation` / `refactor`

---

## 6. Hệ Thống Label

### Loại issue

| Label             | Màu      | Mô tả                       |
| ----------------- | --------- | ----------------------------- |
| `enhancement`   | 🟢 Green  | Tính năng mới              |
| `bug`           | 🔴 Red    | Lỗi cần fix                 |
| `task`          | 🔵 Blue   | Công việc kỹ thuật        |
| `epic`          | 🟣 Purple | Nhóm nhiều issue liên quan |
| `documentation` | ⚪ Gray   | Tài liệu                    |

### Lĩnh vực

| Label         | Mô tả                            |
| ------------- | ---------------------------------- |
| `backend`   | Code phía server (.NET, API)      |
| `frontend`  | Code phía client (Next.js, React) |
| `fullstack` | Cả hai                            |
| `database`  | Migration, schema                  |
| `devops`    | CI/CD, Docker, deploy              |

### Độ ưu tiên

| Label           | Thời gian xử lý                       |
| --------------- | ---------------------------------------- |
| `priority:P0` | Xử lý**ngay lập tức** (< 4h)   |
| `priority:P1` | Xử lý trong**ngày** (< 24h)     |
| `priority:P2` | Xử lý trong**sprint hiện tại** |
| `priority:P3` | Xử lý khi có thời gian               |

### Kích thước

| Label           | Ước lượng |
| --------------- | ------------- |
| `size:small`  | < 2 giờ      |
| `size:medium` | 2-8 giờ      |
| `size:large`  | > 1 ngày     |

---

## 7. Quy Trình Làm Việc Với Issue

```
📝 Tạo Issue → 👤 Assign → 🔨 Làm việc → 🔍 Code Review → ✅ Close
```

### Khi bắt đầu làm:

1. **Self-assign** issue cho mình
2. Tạo branch theo format: `feature/issue-{number}-short-description` hoặc `fix/issue-{number}-short-description`
3. Comment trên issue nếu cần thảo luận

### Khi hoàn thành:

1. Tạo **Pull Request** link đến issue: `Closes #123`
2. Request **Code Review** từ team member
3. Sau khi merge → issue tự động **Close**

### Comment trên issue:

- Cập nhật tiến độ nếu task kéo dài > 1 ngày
- Tag người liên quan bằng `@username`
- Đính kèm screenshot nếu là UI

---

## 8. Ví Dụ Hoàn Chỉnh

### Issue: Feature

**Title:** `[API] Add export CSV endpoint for daily reports`

**Body:**

```markdown
## Mô tả
Thêm endpoint cho phép export báo cáo hàng ngày ra file CSV để gửi cho quản lý.

## Chi tiết yêu cầu
- [ ] Endpoint: `GET /api/reports/daily/{id}/export?format=csv`
- [ ] Trả về file CSV với header: Date, Author, Commits, Additions, Deletions
- [ ] Support date range filter

## Acceptance Criteria
- [ ] API trả về file CSV valid, mở được trong Excel
- [ ] Có unit test cho logic export
- [ ] Đã test với data thật từ Gitea

## Technical Notes
- Dùng `CsvHelper` library
- Reference: ReportsController.cs
```

**Labels:** `enhancement`, `backend`, `priority:P2`, `size:medium`
**Assignee:** `@developer_name`

---

### Issue: Bug

**Title:** `[UI] Dashboard chart không hiển thị trên mobile`

**Body:**

```markdown
## Mô tả lỗi
Chart trên trang Executive Overview bị tràn ra ngoài màn hình khi xem trên mobile.

## Cách tái hiện
1. Mở browser trên điện thoại (hoặc responsive mode < 768px)
2. Truy cập trang chủ Dashboard
3. Scroll xuống phần biểu đồ

## Kết quả thực tế
- Chart bị tràn, phải scroll ngang

## Kết quả mong đợi
- Chart responsive, tự co giãn theo màn hình

## Môi trường
- iPhone 14, Safari 17
- Chrome DevTools responsive (375px)

## Screenshot
(đính kèm ảnh)

## Mức độ nghiêm trọng
- [x] P2 — Ảnh hưởng ít user, có workaround (xem trên desktop)
```

**Labels:** `bug`, `frontend`, `priority:P2`, `size:small`

---

## 9. Quy Định Commit Message

### Format chuẩn (Conventional Commits)

```
<type>(scope): <subject>

[body]

[footer]
```

### Các loại Type

| Type         | Mô tả                                  | Ví dụ                                             |
| ------------ | ---------------------------------------- | --------------------------------------------------- |
| `feat`     | Tính năng mới                         | `feat(auth): add JWT refresh token`               |
| `fix`      | Sửa lỗi                                | `fix(api): handle null response in user endpoint` |
| `refactor` | Refactor code (không thay đổi logic)  | `refactor(service): extract validation to helper` |
| `docs`     | Tài liệu                               | `docs(readme): update deployment instructions`    |
| `style`    | Format code, thiếu dấu chấm phẩy,... | `style(ui): fix indentation in Sidebar`           |
| `test`     | Thêm/sửa test                          | `test(auth): add unit test for login flow`        |
| `chore`    | Build, config, CI/CD                     | `chore(docker): update nginx config`              |
| `perf`     | Cải thiện hiệu năng                  | `perf(query): optimize commit report query`       |
| `hotfix`   | Fix khẩn cấp trên production          | `hotfix(api): fix crash on empty request body`    |

### Quy tắc bắt buộc

| # | Quy tắc                                                      | Bắt buộc          |
| - | ------------------------------------------------------------- | ------------------- |
| 1 | Viết bằng**tiếng Việt (Anh)**                       | ✅                  |
| 2 | Subject**không quá 72 ký tự**                       | ✅                  |
| 3 | Subject**không kết thúc bằng dấu chấm**           | ✅                  |
| 4 | Dùng**thì hiện tại**: "add" không phải "added"    | ✅                  |
| 5 | **Scope** phải rõ ràng: module hoặc tính năng     | ✅                  |
| 6 | Mỗi commit chỉ giải quyết**1 vấn đề**            | ✅                  |
| 7 | Link đến issue nếu có:`Refs #123` hoặc `Closes #123` | ⚠️ Khuyến khích |

### Ví dụ đúng ✅

```
feat(dashboard): add commit report page with sync button

- Add /commit-report page with drill-down view
- Support date range filter (3d, 7d, 14d, 30d)
- Add Sync Now button to trigger manual sync

Refs #45
```

```
fix(blockers): calculate DaysStuck in real-time from issue data

Previously DaysStuck was set once at creation and never updated.
Now queries actual UpdatedAtSource from Issues/PullRequests table.

Closes #78
```

```
refactor(api): extract repo lookup logic into helper method
```

### Ví dụ sai ❌

```
update code                    ← Quá chung chung
fix bug                        ← Không rõ fix gì
WIP                            ← Không nên commit code chưa hoàn chỉnh
Sửa lỗi đăng nhập             ← Phải viết tiếng Anh
feat(auth): Add JWT refresh token.  ← Không viết hoa chữ đầu subject, không dấu chấm
```

### Lưu ý quan trọng

- **Không commit code chưa chạy được** — phải build thành công trước khi commit
- **Không gộp nhiều thay đổi không liên quan** vào 1 commit
- **Squash commits** trước khi merge PR nếu có quá nhiều commit nhỏ lẻ
- Sử dụng `git rebase -i` để gộp/sửa commit message trước khi push

### Branch & Commit Flow

```
main (production)
  └── develop (staging)
        └── feature/issue-123-add-export-csv
              ├── feat(export): add CSV export endpoint
              ├── feat(export): add date range filter
              ├── test(export): add unit tests for CSV export
              └── docs(export): update API docs
```

---

> **Lưu ý**: AI CTO Dashboard tự động đọc issues và commits từ Gitea để phân tích, tạo báo cáo, và theo dõi tiến độ. Việc tạo issue và commit chuẩn format giúp AI phân tích chính xác hơn.
