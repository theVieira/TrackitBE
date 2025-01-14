using Swashbuckle.AspNetCore.SwaggerGen;
using Trackit.Authentication;
using Trackit.Common.Configurations;

namespace Trackit.Common.Injections;

public static class Injections
{
    public static void AddInjections(this WebApplicationBuilder builder)
    {
        builder.AddClientInjection();
        builder.AddTechInjection();
        builder.AddTicketInjection();
        builder.AddAvatarInjection();
        
        builder.Services.AddSingleton<ITokenManager, TokenManager>();
    }
}