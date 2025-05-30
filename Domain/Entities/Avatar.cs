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
    public AvatarType Type { get; init; } =  AvatarType.Default;
    
    // EF
    #pragma warning disable
    protected Avatar() { }
    
    protected Avatar(string url, string filename, string path, AvatarType type)
    {
        Url = url;
        Filename = filename;
        Path = path;
        Type = type;
    }

    public static class Factory
    {
        public static Avatar Create(
            string url,
            string filename,
            string path,
            AvatarType type
        )
        {
            return new Avatar(
                url, filename, path, type
            );
        }
    }
}

public enum AvatarType
{
    Default,
    Client,
    Tech
}