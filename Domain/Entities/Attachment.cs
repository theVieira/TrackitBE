using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class Attachment : BaseEntity
{
    [JsonIgnore]
    [Required]
    public Guid UploadedById { get; init; }
    public Tech UploadedBy { get; init; } = null!;
    [JsonIgnore]
    [Required]
    public Guid TicketId { get; init; }
    [JsonIgnore]
    public Ticket Ticket { get; init; } = null!;
    [Required]
    public string Filename { get; init; }
    [Required]
    public long Size { get; init; }
    [JsonIgnore]
    [Required]
    public string Path { get; init; }
    [Required]
    [Url]
    public string Url { get; init; }    
    
    
    // EF
    #pragma warning disable
    protected Attachment() { }

    protected Attachment(
        Guid ticketId, 
        Guid uploadedById, 
        string filename,
        long size,
        string path,
        string url
    )
    {
        UploadedById = uploadedById;
        TicketId = ticketId;
        Filename = filename;
        Size = size;
        Url = url;
        Path = path;
    }

    public static class Factory
    {
        public static Attachment Create(
            Guid ticketId, 
            Guid uploadedById, 
            string filename,
            long size,
            string path,
            string url
        )
        {
            return new Attachment(
                ticketId,
                uploadedById,
                filename,
                size,
                path,
                url
            );
        }
    }
}