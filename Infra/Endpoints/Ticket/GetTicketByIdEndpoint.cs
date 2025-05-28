using Microsoft.AspNetCore.Mvc;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class GetTicketByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]AppDbContext context
    )
    {
        if (!Guid.TryParse(id, out Guid ticketId))
            return Results.NotFound();

        var ticket = await context.Tickets.FindAsync(ticketId);
        
        return Results.Ok(ticket);
    }
}
