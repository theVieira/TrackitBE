using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Application.Services;

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
    public eTicketCategory Category { get; private set; }
    [Required]
    public eTicketStatus Status { get; private set; }
    [Required]
    public eTicketPriority Priority { get; private set; }
    [MinLength(10, ErrorMessage = "Ticket description min length is 10")]
    [MaxLength(500, ErrorMessage = "Ticket description max length is 500")]
    public string Description { get; private set; }
    [Required]
    public eTicketTag Tag { get; private set; }
    [Required]
    public bool Recurrent { get; private set; }
    public ICollection<Note> Notes { get; init; } = [];
    public ICollection<Feedback> Feedbacks { get; init; } = [];
    public ICollection<Progress> Progress { get; init; } = [];
    public ICollection<Finish> Finish { get; init; } = [];
    public ICollection<Reopen> Reopen { get; init; } = [];
    public ICollection<TicketAttachment> Attachments { get; init; } = [];
    
    // EF
    #pragma warning disable
    private Ticket() { }

    private Ticket(
        Guid clientId, 
        Guid createdById, 
        string description, 
        eTicketTag tag,
        eTicketCategory category,
        eTicketPriority priority
    )
    {
        ClientId = clientId;
        CreatedById = createdById;
        Description = SpellCheckService.CapitalizeText(description);
        Tag = tag;
        Recurrent = false;
        Status = eTicketStatus.Open;
        Priority = priority;
        Category = category;
    }

    public void FinalizeTicket(Finish finish)
    {
        if (Status is not eTicketStatus.Progress)
            throw new Exception("Ticket status must be progress to update the status for finish");

        Status = eTicketStatus.Finish;
    }
    
    public void ToMeetTicket()
    {
        if(Status is not eTicketStatus.Open)
            throw new Exception("Ticket status must be open to update the status for progress");
        
        Status = eTicketStatus.Progress;
    }
    
    public void ChangeCategory(eTicketCategory category)
    {
        if (Category == category)
            throw new Exception("Category already this is");

        Status = eTicketStatus.Open;
        Category = category;
    }

    public void AddAttachment(TicketAttachment attachment)
    {
        this.Attachments.Add(attachment);
    }
    
    public static class Factory
    {
        public static Ticket Create(
            Guid clientId, 
            Guid createdById, 
            string description, 
            eTicketTag tag,
            eTicketCategory category,
            eTicketPriority priority
        )
        {
            return new Ticket(
                clientId, createdById, description, tag, category, priority
            );
        }
    }
}
