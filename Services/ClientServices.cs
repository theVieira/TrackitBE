using Trackit.Core.Gateways;
using Trackit.Core.UseCases;
using Trackit.Infra.Persistence;

namespace Trackit.Services;

public static class ClientServices
{
  public static void AddClientServices(this IServiceCollection services)
  {
    services.AddScoped<IClientGateway, ClientRepository>();

    services.AddScoped<CreateClientUseCase>();
  }
}