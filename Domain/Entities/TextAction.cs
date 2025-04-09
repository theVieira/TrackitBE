using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Utils;

namespace Trackit.Domain.Entities;

public class TextAction : BaseEntity
{
    public Tech Author { get; init; } = null!;
    public Guid AuthorId { get; init; }
    
    [MinLength(10, ErrorMessage = "Ticket note min length is 10")]
    [MaxLength(500, ErrorMessage = "Ticket note max length is 500")]
    public string Content { get; private set; }
    [JsonIgnore]
    public TextActionType Type { get; init; }
    [JsonIgnore]
    public Guid TicketId { get; init; }
    public Ticket Ticket { get; init; } = null!;
    
    // EF
    #pragma warning disable
    protected TextAction() { }

    protected TextAction(
        Guid authorId,
        Guid ticketId,
        TextActionType type,
        string content
    )
    {
        TicketId = ticketId;
        AuthorId = authorId;
        Content = SpellCheck.CapitalizeName(content);
        Type = type;
    }

    public static class Factory
    {
        public static TextAction CreateNote(
            Guid authorId,
            Guid ticketId,
            string note
        )
        {
            return new TextAction(
                authorId, ticketId, TextActionType.Note, note        
            );
        }
        
        public static TextAction CreateFeedback(
            Guid authorId, 
            Guid ticketId,
            string note
        )
        {
            return new TextAction(
                authorId, ticketId, TextActionType.Feedback, note        
            );
        }
    }
}

public enum TextActionType
{
    Default,
    Note,
    Feedback
}