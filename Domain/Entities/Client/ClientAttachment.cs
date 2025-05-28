using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public class ClientAttachment : Attachment
{
    [Required]
    [JsonIgnore]
    public Guid ClientId { get; init; }

    [Required] [JsonIgnore] public Client Client { get; init; } = null!;
}