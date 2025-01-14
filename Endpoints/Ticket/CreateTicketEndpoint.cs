using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class CreateTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        HttpContext httpContext,
        [FromBody]CreateTicketRequest request,
        [FromServices]ITokenManager tokenManager,
        [FromServices]IClient clientContext,
        [FromServices]ITech techContext,
        [FromServices]ITicket ticketContext
    )
    {
        var authHeader = httpContext.Request.Headers["Authorization"].ToString();
        
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            return Results.Unauthorized();

        var token = authHeader["Bearer ".Length..];

        var claimValue = tokenManager.GetClaimValue(token, ClaimTypes.NameIdentifier);

        if (
        !Guid.TryParse(request.ClientId, out var clientId)
            ||
        !Guid.TryParse(claimValue, out var techId)
        ) return Results.Unauthorized();

        var client = await clientContext.FindByIdAsync(clientId);
        
        if (client == null) return Results.NotFound("Client not found");
        
        var ticket = Domain.Entities.Ticket.Factory.Create(
            clientId,
            techId,
            request.Description,
            request.Tag,
            request.Category,
            request.Priority
        );

        await ticketContext.AddAsync(ticket);
        
        return Results.Created();
    }
}

public record CreateTicketRequest(
    string ClientId,
    string Description,
    Priority Priority,
    Category Category,
    TicketTag Tag
);