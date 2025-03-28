using Serilog;
using Serilog.Events;

namespace Trackit.Common.Configurations;

public static class SerilogConfiguration
{
    public static void AddSerilogConfiguration(this ConfigureHostBuilder host)
    {
        var directoryLogs = Directory.GetCurrentDirectory() + "logs/";

        host.UseSerilog
        (
            (ctx, lc) =>
            {
                lc
                    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning)
                    .WriteTo.File(
                        directoryLogs,
                        LogEventLevel.Warning,
                        rollingInterval: RollingInterval.Day
                    );
            }
        );
    }
}