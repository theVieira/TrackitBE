using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Authentication;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class SetProgressEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("progress/{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]AppDbContext context,
        TokenService tokenService,
        HttpContext httpContext
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);

        var tech = await context.Techs.FindAsync(techId);
        if(tech is null) return Results.Unauthorized();
        
        if(!Guid.TryParse(id, out Guid ticketId))
            return Results.BadRequest();

        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket is null) return Results.NotFound();

        var progress = TimeAction.Factory.CreateProgress(
            tech.Id, ticket.Id
        );
        
        ticket.ToMeetTicket();
        
        context.Tickets.Update(ticket);
        await context.TimeActions.AddAsync(progress);
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}