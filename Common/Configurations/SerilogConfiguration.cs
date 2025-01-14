using Serilog;
using Serilog.Events;

namespace Trackit.Common.Configurations;

public static class SerilogConfiguration
{
    public static void AddSerilogConfiguration(this ConfigureHostBuilder host)
    {
        host.UseSerilog
        (
            (ctx, lc) =>
            {
                lc
                    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning)
                    .WriteTo.File(
                        Directory.GetCurrentDirectory() + "logs",
                        LogEventLevel.Warning,
                        rollingInterval: RollingInterval.Day
                    );
            }
        );
    }
}