using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class AddTicketNoteEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("{id}/note", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]AppDbContext context,
        [FromBody]AddTicketNoteRequest request,
        TokenService tokenService,
        HttpContext httpContext
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);

        var tech = await context.Techs.FindAsync(techId);
        if(tech is null) return Results.Unauthorized();

        if(!Guid.TryParse(id, out var ticketId)) return Results.BadRequest();
        
        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket is null) return Results.NotFound("Ticket not found");

        var note = TextAction.Factory.CreateNote(
            tech.Id,
            ticket.Id,
            request.Content
        );
        
        await context.TextActions.AddAsync(note);
        await context.SaveChangesAsync();
        
        return Results.Created("", note);
    }
}

public record AddTicketNoteRequest(
    string Content
);