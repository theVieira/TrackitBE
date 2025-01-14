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
    
    // EF
    private Avatar() { }
    
    private Avatar(string url, string filename, string path)
    {
        Url = url;
        Filename = filename;
        Path = path;
    }

    public static class Factory
    {
        public static Avatar Create(
            string url,
            string filename,
            string path
        )
        {
            return new Avatar(
                url, filename, path    
            );
        }
    }
}