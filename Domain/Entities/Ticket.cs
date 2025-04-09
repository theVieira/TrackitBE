using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Utils;

namespace Trackit.Domain.Entities;

public class Ticket : BaseEntity
{
    public Client Client { get; init; } = null!;
    [Required]
    public Guid ClientId { get; init; }
    public Tech CreatedBy { get; init; } = null!;
    [Required]
    public Guid CreatedById { get; init; }
    [Required]
    public Category Category { get; private set; }
    [Required]
    public Status Status { get; private set; }
    [Required]
    public Priority Priority { get; private set; }
    [MinLength(10, ErrorMessage = "Ticket description min length is 10")]
    [MaxLength(500, ErrorMessage = "Ticket description max length is 500")]
    public string Description { get; private set; }
    [Required]
    public TicketTag Tag { get; private set; }
    [Required]
    public bool Recurrent { get; private set; }
    public ICollection<Note> Notes { get; init; } = [];
    public ICollection<Feedback> Feedbacks { get; init; } = [];
    public ICollection<Progress> Progress { get; init; } = [];
    public ICollection<Finish> Finish { get; init; } = [];
    public ICollection<Reopen> Reopen { get; init; } = [];
    public ICollection<Attachment> Attachments { get; init; } = [];
    
    // EF
    #pragma warning disable
    private Ticket() { }

    private Ticket(
        Guid clientId, 
        Guid createdById, 
        string description, 
        TicketTag tag,
        Category category,
        Priority priority
    )
    {
        ClientId = clientId;
        CreatedById = createdById;
        Description = SpellCheck.CapitalizeText(description);
        Tag = tag;
        Recurrent = false;
        Status = Status.Open;
        Priority = priority;
        Category = category;
    }

    public void SetProgress(Progress progress)
    {
        if(Status is not Status.Open)
            throw new Exception("Ticket status must be open to update the status for progress");
        
        Status = Status.Progress;
        Progress.Add(progress);
    }
    
    public static class Factory
    {
        public static Ticket Create(
            Guid clientId, 
            Guid createdById, 
            string description, 
            TicketTag tag,
            Category category,
            Priority priority
        )
        {
            return new Ticket(
                clientId, createdById, description, tag, category, priority
            );
        }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketTag
{
    Critical,
    NetworkFailure,
    HardwareFailure,
    SoftwareFailure,
    Maintenance
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    Daily,
    Maintenance,
    Budget,
    Delivery
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Open,
    Progress,
    Finish,
    Cancelled
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Priority
{
    Low,
    Medium,
    High,
    Urgent
}

public class Note : TextAction { }
public class Feedback : TextAction { }
public class Progress : TimeAction { }
public class Finish : TimeAction { }
public class Reopen : TimeAction { }
