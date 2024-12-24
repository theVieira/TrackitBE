using Trackit.Core.Gateways;
using Trackit.Core.UseCases;
using Trackit.Infra.Persistence;

namespace Trackit.Services;

public static class TechServices
{
  public static void AddTechServices(this IServiceCollection services)
  {
    services.AddScoped<ITechGateway, TechRepository>();

    services.AddScoped<CreateTechUseCase>();
  }
}