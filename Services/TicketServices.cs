using Trackit.Core.Gateways;
using Trackit.Core.UseCases;
using Trackit.Infra.Persistence;

namespace Trackit.Services;

public static class TicketServices
{
  public static void AddTicketServices(this IServiceCollection services)
  {
    services.AddScoped<ITicketGateway, TicketRepository>();

    services.AddScoped<CreateTicketUseCase>();
    services.AddScoped<ListTicketsUseCase>();
    services.AddScoped<DeleteTicketUseCase>();
  }
}