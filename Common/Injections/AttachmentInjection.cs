using Trackit.Domain.Interfaces;
using Trackit.Infra.Persistence.Repositories;

namespace Trackit.Common.Injections;

public static class AttachmentInjection
{
    public static void AddAttachmentInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAttachment, AttachmentRepository>();
    }
}