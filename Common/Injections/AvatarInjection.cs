using Trackit.Domain.Interfaces;
using Trackit.Infra.Persistence.Repositories;

namespace Trackit.Common.Injections;

public static class AvatarInjection
{
    public static void AddAvatarInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAvatar, AvatarRepository>();
    }
}