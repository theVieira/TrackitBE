using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class Avatar : BaseEntity
{
    [Url]
    [Required]
    public string Url { get; private set; }
    [Required]
    public string Filename { get; private set; }
    [JsonIgnore]
    [Required]
    public string Path { get; private set; }
    [Required]
    [JsonIgnore]
    public Guid TechId { get; init; }
    [JsonIgnore]
    public Tech Tech { get; init; }
    
    // EF
    #pragma warning disable
    private Avatar() { }
    
    private Avatar(string url, string filename, string path, Guid techId)
    {
        Url = url;
        Filename = filename;
        Path = path;
        TechId = techId;
    }

    public static class Factory
    {
        public static Avatar Create(
            string url,
            string filename,
            string path,
            Guid techId
        )
        {
            return new Avatar(
                url, filename, path, techId
            );
        }
    }
}