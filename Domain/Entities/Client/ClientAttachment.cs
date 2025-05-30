using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class ClientAttachment : Attachment
{
    [Required]
    [JsonIgnore]
    public Guid ClientId { get; init; }
    [Required]
    [JsonIgnore] 
    public Client Client { get; init; } = null!;
    
    // EF
    #pragma warning disable
    private ClientAttachment() : base() { }

    private ClientAttachment(
        Guid uploadedById,
        string filename,
        long size,
        string path,
        string url,
        Guid clientId
    ) : base(uploadedById, filename, size, path, url, AttachmentType.Client)
    {
        ClientId = clientId;
    }

    public static class Factory
    {
        public static ClientAttachment Create(
            Guid uploadedById,
            string filename,
            long size,
            string path,
            string url,
            Guid clientId
        )
        {
            return new ClientAttachment(
                uploadedById,
                filename,
                size,
                path,
                url,
                clientId
            );
        }
    }
}