using System.Text.Json.Serialization;
using Microsoft.Build.Framework;

namespace Trackit.Domain.Entities;
 
public class TechAvatar : Avatar
{
    [Required]
    [JsonIgnore]
    public Guid TechId { get; init; }
    [Required]
    [JsonIgnore]
    public Tech Tech { get; init; } = null!;
    
    private TechAvatar() : base() {}

    private TechAvatar(string url, string filename, string path, Guid techId) : base(url, filename, path)
    {
        TechId = techId;
    }

    public static class Factory
    {
        public static TechAvatar Create(
            string url, string filename, string path, Guid techId
        )
        {
            return new TechAvatar(
                url, filename, path, techId
            );
        }
    }
}