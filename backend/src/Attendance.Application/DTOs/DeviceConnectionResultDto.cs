using Attendance.Domain.Enums;

namespace Attendance.Application.DTOs;

public sealed record DeviceConnectionResultDto(
    bool Connected,
    int ResponseTimeMs,
    DeviceConnectionStatus ConnectionStatus,
    string? ErrorMessage = null);