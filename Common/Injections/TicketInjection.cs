using Trackit.Domain.Interfaces;
using Trackit.Infra.Persistence.Repositories;

namespace Trackit.Common.Injections;

public static class TicketInjection
{
    public static void AddTicketInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITicket, TicketRepository>();
    }
}