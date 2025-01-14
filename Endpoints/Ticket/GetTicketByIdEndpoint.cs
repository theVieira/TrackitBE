using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class GetTicketByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        [FromServices]ITicket context
    )
    {
        if (!Guid.TryParse(id, out Guid ticketId))
            return Results.NotFound();

        var ticket = await context.FindByIdAsync(ticketId);
        return Results.Ok(ticket);
    }
}
