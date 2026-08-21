using Attendance.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<AttendanceWorker>();

var host = builder.Build();
host.Run();
