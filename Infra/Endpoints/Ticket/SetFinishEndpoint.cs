using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Trackit.Application.Services;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class SetFinishEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("{id}/finish", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromBody]SetFinishRequest request,
        [FromServices]AppDbContext context,
        HttpContext httpContext,
        [FromServices]TokenService tokenService
    )
    {
        var techId = tokenService.GetClaimValueFromRequest(ClaimTypes.NameIdentifier, httpContext);
        
        var tech = await context.Techs.FindAsync(techId);
        if(tech is null) return Results.Unauthorized();
        
        if(!Guid.TryParse(id, out var ticketId)) return  Results.BadRequest();
        
        var ticket = await context.Tickets.FindAsync(ticketId);
        if(ticket is null) return Results.NotFound();

        var feedback = TextAction.Factory.CreateFeedback(
            tech.Id,
            ticket.Id,
            request.Feedback
        );
        
        await context.TextActions.AddAsync(feedback);

        var finish = TimeAction.Factory.CreateFinish(tech.Id, ticket.Id);
        ticket.FinalizeTicket((finish as Finish)!);
        
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}

public record SetFinishRequest(string Feedback);