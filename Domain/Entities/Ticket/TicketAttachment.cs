using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class TicketAttachment : Attachment
{
    [Required]
    [JsonIgnore]
    public Guid TicketId { get; init; }
    [JsonIgnore]
    public Ticket Ticket { get; init; }
    
    // EF
    #pragma warning disable
    private TicketAttachment() : base() { }

    private TicketAttachment(
        Guid uploadedById, 
        Guid ticketId,
        string filename,
        long size,
        string path,
        string url
    ) : base(uploadedById, filename, size, path, url, AttachmentType.Ticket)
    {
        TicketId = ticketId;
    }
    
    public static class Factory
    {
        public static TicketAttachment Create(
            Guid uploadedById, 
            Guid ticketId,
            string filename,
            long size,
            string path,
            string url    
        )
        {
            return new TicketAttachment(
                uploadedById, ticketId, filename, size, path, url
            );
        }
    }
}
