using Trackit.Domain.Interfaces;
using Trackit.Infra.Persistence.Repositories;

namespace Trackit.Common.Injections;

public static class ClientInjection
{
    public static void AddClientInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IClient, ClientRepository>();
    }
}