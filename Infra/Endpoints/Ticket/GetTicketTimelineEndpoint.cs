using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Endpoints;
using Trackit.Infra.Persistence;

namespace Trackit.Infra.Endpoints.Ticket;

public class GetTicketTimelineEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("{id}/timeline", HandleAsync);

    private static async Task<IResult> HandleAsync(
        string id,
        AppDbContext context
    )
    {
        if(!Guid.TryParse(id, out var Id)) return Results.BadRequest();
        
        var ticket = await context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.Progress).ThenInclude(timeAction => timeAction.Author)
            .Include(t => t.Reopen).ThenInclude(timeAction => timeAction.Author)
            .Include(t => t.Finish).ThenInclude(timeAction => timeAction.Author)
            .Include(t => t.Attachments)
            .Include(t => t.Feedbacks)
            .Include(t => t.Notes)
            .FirstOrDefaultAsync(t => t.Id == Id);
        
        if(ticket is null) return  Results.NotFound();

        var timeline = new List<TicketTimeline>();

        timeline.Add(
            TicketTimeline.Factory.Create(
              ticket.CreatedAt,
              eTicketTimelineType.Create,
              ticket.CreatedBy,
              ""
            )
        );
        
        timeline.AddRange(ticket.Progress.Select(
                p => TicketTimeline.Factory.Create(
                    p.CreatedAt,
                    eTicketTimelineType.Progress,
                    p.Author,
                    ""
                )
            )
        );
        
        timeline.AddRange(ticket.Finish.Select(
                p => TicketTimeline.Factory.Create(
                    p.CreatedAt,
                    eTicketTimelineType.Finish,
                    p.Author,
                    ""
                )
            )
        );
        
        timeline.AddRange(ticket.Reopen.Select(
                p => TicketTimeline.Factory.Create(
                    p.CreatedAt,
                    eTicketTimelineType.Reopen,
                    p.Author,
                    ""
                )
            )
        );
        
        return Results.Ok(timeline.OrderBy(t => t.CreatedAt).ToList());
    }
}