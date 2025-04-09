using Trackit.Endpoints;
using Trackit.Endpoints.Authentication;
using Trackit.Endpoints.Client;
using Trackit.Endpoints.Tech;
using Trackit.Endpoints.Ticket;

namespace Trackit.Common.Extensions.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/sign-in")
            .WithTags("Authentication")
            .MapEndpoint<AuthenticationEndpoint>();
        
        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Ok" });

        endpoints.MapGroup("/clients")
            .WithTags("Clients")
            .MapEndpoint<CreateClientEndpoint>()
            .MapEndpoint<GetClientsEndpoint>()
            .MapEndpoint<GetClientByIdEndpoint>();

        endpoints.MapGroup("/techs")
            .WithTags("Techs")
            .MapEndpoint<CreateTechEndpoint>()
            .MapEndpoint<GetTechsEndpoint>()
            .MapEndpoint<GetTechByTokenEndpoint>()
            .MapEndpoint<EditTechAvatarEndpoint>();

        endpoints.MapGroup("/tickets")
            .WithTags("Tickets")
            .MapEndpoint<GetTicketsEndpoint>()
            .MapEndpoint<CreateTicketEndpoint>()
            .MapEndpoint<DeleteTicketEndpoint>()
            .MapEndpoint<GetTicketByIdEndpoint>()
            .MapEndpoint<AddAttachmentEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app) where T : IEndpoint
    {
        T.Map(app);
        
        return app;
    }
}