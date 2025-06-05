using Trackit.Endpoints;
using Trackit.Endpoints.Client;
using Trackit.Endpoints.Ticket;
using Trackit.Infra.Endpoints.Authentication;
using Trackit.Infra.Endpoints.Client;
using Trackit.Infra.Endpoints.File;
using Trackit.Infra.Endpoints.Tech;
using Trackit.Infra.Endpoints.Ticket;

namespace Trackit.Common.Extensions.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/sign-in")
            .WithTags("Authentication")
            .MapEndpoint<SignInEndpoint>();
        
        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("/clients")
            .WithTags("Clients")
            .MapEndpoint<CreateClientEndpoint>()
            .MapEndpoint<GetClientsEndpoint>()
            .MapEndpoint<GetClientByIdEndpoint>()
            .MapEndpoint<EditClientAvatarEndpoint>()
            .MapEndpoint<GetAllClientsEndpoint>();

        endpoints.MapGroup("/techs")
            .WithTags("Techs")
            .MapEndpoint<CreateTechEndpoint>()
            .MapEndpoint<GetTechsEndpoint>()
            .MapEndpoint<EditTechAvatarEndpoint>();

        endpoints.MapGroup("/tickets")
            .WithTags("Tickets")
            .MapEndpoint<GetTicketsEndpoint>()
            .MapEndpoint<CreateTicketEndpoint>()
            .MapEndpoint<DeleteTicketEndpoint>()
            .MapEndpoint<GetTicketByIdEndpoint>()
            .MapEndpoint<AddAttachmentEndpoint>()
            .MapEndpoint<DownloadFileEndpoint>()
            .MapEndpoint<SetProgressEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        
        return app;
    }
}