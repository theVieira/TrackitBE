using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class Attachment : BaseEntity
{
    [JsonIgnore]
    [Required]
    public Guid UploadedById { get; init; }
    public Tech UploadedBy { get; init; }
    public string Filename { get; init; }
    [Required]
    public long Size { get; init; }
    [JsonIgnore]
    [Required]
    public string Path { get; init; }
    [Required]
    [Url]
    public string Url { get; init; }    
    [Required]
    public AttachmentType Type {  get; init; }
    
    
    // EF
    #pragma warning disable
    protected Attachment() { }

    protected Attachment(
        Guid uploadedById, 
        string filename,
        long size,
        string path,
        string url
    )
    {
        UploadedById = uploadedById;
        Filename = filename;
        Size = size;
        Url = url;
        Path = path;
    }

    public static class Factory
    {
        public static Attachment Create(
            Guid uploadedById, 
            string filename,
            long size,
            string path,
            string url
        )
        {
            return new Attachment(
                uploadedById,
                filename,
                size,
                path,
                url
            );
        }
    }
}

public enum AttachmentType
{
    Client,
    Ticket
}