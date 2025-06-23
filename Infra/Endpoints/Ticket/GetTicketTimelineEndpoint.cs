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
            .Include(t => t.Feedbacks).ThenInclude(textAction => textAction.Author)
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

        var finishTimeline = await Task.WhenAll(
            ticket.Finish.Select(async p =>
            {
                var feedback = await context.TextActions
                    .Where(t =>
                        t.Type == TextActionType.Feedback && t.TicketId == p.TicketId
                    )
                    .OrderByDescending(t => t.CreatedAt)
                    .FirstOrDefaultAsync();

                return TicketTimeline.Factory.Create(
                    p.CreatedAt,
                    eTicketTimelineType.Finish,
                    p.Author,
                    feedback?.Content
                );
            })
        );
        
        timeline.AddRange(finishTimeline);
        
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