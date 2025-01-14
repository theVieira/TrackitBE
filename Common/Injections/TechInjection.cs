using Trackit.Domain.Interfaces;
using Trackit.Infra.Persistence.Repositories;

namespace Trackit.Common.Injections;

public static class TechInjection
{
    public static void AddTechInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITech, TechRepository>();
    }
}