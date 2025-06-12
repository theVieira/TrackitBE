using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        var ticket = await context.Tickets
            .Include(t => t.Notes)
            .Include(t => t.Feedbacks)
            .Include(t => t.Attachments)
            .Include(t => t.Finish)
            .Include(t => t.Progress)
            .Include(t => t.Reopen)
            .Include(t => t.CreatedBy)
                .ThenInclude(t => t.Avatar)
            .Include(t => t.Client)
                .ThenInclude(c => c.Avatar)
            .FirstOrDefaultAsync(t => t.Id == ticketId);
        
        return Results.Ok(ticket);
    }
}
