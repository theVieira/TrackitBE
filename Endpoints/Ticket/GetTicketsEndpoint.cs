using Microsoft.AspNetCore.Mvc;
using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Endpoints.Ticket;

public class GetTicketsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]int skip,
        [FromQuery]int take,
        [FromQuery]Status[] status,
        [FromQuery]Category[] category,
        [FromQuery]Priority[] priority,
        [FromQuery]string? client,
        [FromServices]ITicket context
    )
    {
        if (client is not null && client.Length > 0)
        {
            var filtredTickets = await context
                .ListByClientAsync(skip, take, new TicketFilters(status, category, priority), client);
        
            return Results.Ok(filtredTickets);
        }
        
        var allTickets = await context
            .ListAsync(skip, take, new TicketFilters(status, category, priority));
        
        return Results.Ok(allTickets);
    }
}