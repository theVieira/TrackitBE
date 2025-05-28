using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class CreateTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]CreateTicketRequest request,
        [FromServices]AppDbContext context,
        HttpContext httpContext,
        TokenService tokenService
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);
        var tech =  await context.Techs.FindAsync(techId);
        if (tech == null) return Results.Unauthorized();
        
        if (!Guid.TryParse(request.ClientId, out var clientId)) return Results.Unauthorized();

        var client = await context.Clients.FindAsync(clientId);
        if (client == null) return Results.NotFound("Client not found");
        
        var ticket = Domain.Entities.Ticket.Factory.Create(
            client.Id,
            tech.Id,
            request.Description,
            request.Tag,
            request.Category,
            request.Priority
        );

        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();
        
        return Results.Created();
    }
}

public record CreateTicketRequest(
    string ClientId,
    string Description,
    eTicketPriority Priority,
    eTicketCategory Category,
    eTicketTag Tag
);