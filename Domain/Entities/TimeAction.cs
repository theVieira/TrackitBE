using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class TimeAction : BaseEntity
{
    public Tech Author { get; init; } = null!;
    [JsonIgnore]
    public Guid AuthorId { get; init; }
    [JsonIgnore]
    public Guid TicketId { get; init; }
    public Ticket Ticket { get; init; } = null!;
    public TimeActionType Type { get; init; }
    
    // EF
    #pragma warning disable
    protected TimeAction() { }

    protected TimeAction(
        Guid authorId,
        Guid ticketId,
        TimeActionType type
    )
    {
        AuthorId = authorId;
        Type = type;
        TicketId = ticketId;
    }

    public static class Factory
    {
        public static TimeAction CreateProgress(
            Guid authorId, Guid ticketId
        )
        {
            return new TimeAction(
                authorId, ticketId, TimeActionType.Progress
            );
        }
        
        public static TimeAction CreateFinish(
            Guid authorId, Guid ticketId
        )
        {
            return new TimeAction(
                authorId, ticketId, TimeActionType.Finish
            );
        }
        
        public static TimeAction CreateReopen(
            Guid authorId, Guid ticketId
        )
        {
            return new TimeAction(
                authorId, ticketId, TimeActionType.Reopen
            );
        }
    }
}

public enum TimeActionType
{
    Default,
    Progress,
    Finish,
    Reopen
}