using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class DeleteTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]DeleteTicketRequest req,
        [FromServices]ITicket context
    )
    {
        var ticket = await context.FindByIdAsync(req.TicketId);
        
        if(ticket is null) return Results.NotFound();
        
        ticket.Delete();

        await context.UpdateAsync(ticket);
        
        return Results.NoContent();
    }
}

public record DeleteTicketRequest(Guid TicketId);