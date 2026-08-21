namespace Attendance.Domain.Enums;

public enum AttendanceStatus
{
    PRESENT,
    ABSENT,
    LATE,
    EARLY_LEAVE,
    LATE_AND_EARLY,
    MISSING_CHECKIN,
    MISSING_CHECKOUT,
    NO_SHIFT,
    LEAVE,
    HOLIDAY,
    BUSINESS_TRIP,
    REMOTE_WORK,
    OVERTIME
}
