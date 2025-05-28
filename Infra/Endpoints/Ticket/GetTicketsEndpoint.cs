using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class GetTicketsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        [FromQuery]int skip,
        [FromQuery]int take,
        [FromQuery]eTicketStatus[] status,
        [FromQuery]eTicketCategory[] category,
        [FromQuery]eTicketPriority[] priority,
        [FromQuery]string? client,
        [FromServices]AppDbContext context
    )
    {
        var query = context.Tickets.AsQueryable();

        query = query
            .Where(t => status.Contains(t.Status))
            .Where(t => priority.Contains(t.Priority))
            .Where(t => category.Contains(t.Category));
        
        query = query
            .Include(t => t.Progress)
            .Include(t => t.Reopen)
            .Include(t => t.Finish)
            .Include(t => t.Attachments)
            .Include(t => t.Feedbacks)
            .Include(t => t.Notes);
        
        if (!string.IsNullOrEmpty(client))
        {
           query = query.Where(t => t.Client.Name.ToLower() == client.ToLower());
        }
        
        var tickets = query
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        var count = query.CountAsync();

        await Task.WhenAll(tickets, count);
        
        return Results.Ok((items: tickets, total: count));
    }
}