namespace Attendance.Application.Interfaces;

public interface IAttendanceProcessingEngine
{
    Task ProcessAsync(CancellationToken cancellationToken = default);
}
