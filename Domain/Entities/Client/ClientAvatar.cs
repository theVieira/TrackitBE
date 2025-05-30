using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class ClientAvatar : Avatar
{
    [Required]
    [JsonIgnore]
    public Guid ClientId { get; init; }
    [Required]
    [JsonIgnore]
    public Client Client { get; init; } = null!;
    
    // EF
    #pragma warning disable
    private ClientAvatar() : base() {}

    private ClientAvatar(string url, string filename, string path, Guid clientId) : base (url, filename, path, AvatarType.Client)
    {
        ClientId = clientId;
    }

    public static class Factory
    {
        public static ClientAvatar Create(string url, string filename, string path, Guid clientId)
        {
            return new ClientAvatar(url, filename, path, clientId);
        }
    }
}