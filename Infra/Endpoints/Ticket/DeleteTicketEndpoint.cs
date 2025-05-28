using Microsoft.AspNetCore.Mvc;
using Trackit.Infra.Persistence;

namespace Trackit.Endpoints.Ticket;

public class DeleteTicketEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromBody]DeleteTicketRequest request,
        [FromServices]AppDbContext context
    )
    {
        var ticket = await context.Tickets.FindAsync(request.TicketId);
        
        if(ticket is null) return Results.NotFound();
        
        ticket.Delete();

        context.Tickets.Update(ticket);
        await context.SaveChangesAsync();
        
        return Results.NoContent();
    }
}

public record DeleteTicketRequest(Guid TicketId);