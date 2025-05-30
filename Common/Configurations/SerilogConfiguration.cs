using Serilog;
using Serilog.Events;

namespace Trackit.Common.Configurations;

public static class SerilogConfiguration
{
    public static void AddSerilogConfiguration(this ConfigureHostBuilder host)
    {
        var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var logDir = Path.Combine(Directory.GetCurrentDirectory(), "logs", today);

        host.UseSerilog((ctx, config) =>
        {
            config.MinimumLevel.Debug()
                .Enrich.FromLogContext()

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File(
                        Path.Combine(logDir, "error.log"),
                        rollingInterval: RollingInterval.Infinite))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File(
                        Path.Combine(logDir, "warning.log"),
                        rollingInterval: RollingInterval.Infinite))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.File(
                        Path.Combine(logDir, "info.log"),
                        rollingInterval: RollingInterval.Infinite))

                .WriteTo.Console(LogEventLevel.Warning);
        });
    }
}