namespace Attendance.Worker.Services;

public sealed class AttendanceWorker(ILogger<AttendanceWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Attendance worker started in no-op mode.");

        try
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Attendance worker is stopping.");
        }
    }
}
