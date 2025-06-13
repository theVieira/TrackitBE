namespace Trackit.Domain.Entities;

public class TicketTimeline : BaseEntity
{
    public eTicketTimelineType EventType { get; init; }
    public Tech Author { get; init; }
    public string? Content { get; private set; }
    
    #pragma warning disable
    private TicketTimeline() { }

    private TicketTimeline(
        DateTime createdAt,
        eTicketTimelineType eventType,
        Tech author,
        string? content
    )
    {
        EventType = eventType;
        CreatedAt = createdAt;
        Author = author;
        Content = content;
    }

    public static class Factory
    {
        public static TicketTimeline Create(
            DateTime createdAt,
            eTicketTimelineType eventType,
            Tech author,
            string? content
        )
        {
            return new TicketTimeline(createdAt, eventType, author, content);
        }
    }
}