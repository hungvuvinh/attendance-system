# Plan Tracker

| ID | Plan | Status | Notes |
|---|---|---|---|
| sprint-0-foundation | `docs/plans/2026-08-20-sprint-0-foundation.md` | complete-reduced-scope | Final verification pass: backend build pass and 8 tests pass; frontend install/build pass; `attendance_db` has no tables; dependency graph and deferred-scope checks pass. Original acceptance items for Git commit, Docker, persistence/DbContext/migrations and mock device remain intentionally unmet/deferred. Known risks: npm audit reports 3 high vulnerabilities and NuGet reports a Microsoft.OpenApi vulnerability. Targeting net10.0. |
| sprint-1-employee-device-mock | `docs/plans/2026-08-22-sprint-1-employee-device-mock.md` | in-progress | Task 1 (Domain Entities & Enums) completed and verified: added `Department`, `DeviceVendor`, `DeviceConnectionStatus`; expanded `Employee` and `AttendanceDevice` with required Sprint 1 fields/validations; added `MasterDataEntityTests` including attendance code format, case-insensitive unique invariants, and default port 4370. Verification: `dotnet test backend/tests/Attendance.Domain.Tests/` passed (16/16). Remaining tasks: Task 2-7. |
