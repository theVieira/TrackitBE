using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class ReopenTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/reopen", HandleAsync);

    private static async Task<IResult> HandleAsync(
        HttpContext httpContext,
        [FromServices]TokenService tokenService,
        [FromBody]ReopenTicketRequest request,
        [FromServices]AppDbContext context
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);
        var tech = await context.Techs.FindAsync(techId);
        if(tech is null) return Results.Unauthorized();
        
        if(!Guid.TryParse(request.TicketId, out var ticketId)) return Results.BadRequest();
        
        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket is null) return Results.NotFound();

        var reopen = TimeAction.Factory.CreateReopen(tech.Id, ticket.Id);
        
        await context.TimeActions.AddAsync(reopen);
        
        ticket.ReopenTicket();
        
        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}

public record ReopenTicketRequest(string TicketId);