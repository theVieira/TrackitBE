using Trackit.Core.Gateways;
using Trackit.Infra.Lib;

public static class TelegramServices
{
  public static void AddTelegramServices(this IServiceCollection services)
  {
    services.AddScoped<IBotMessage, TelegramBot>();
  }
}