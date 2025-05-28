using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Application.Services;

namespace Trackit.Domain.Entities;

public sealed class Client : BaseEntity
{
    [MinLength(3, ErrorMessage = "Client name min length 3 characters")]
    [MaxLength(50, ErrorMessage = "Client name max 50 characters")]
    public string Name { get; private set; }
    [MinLength(14, ErrorMessage = "Client cnpj min length 14 characters")]
    [MaxLength(18, ErrorMessage = "Client cnpj max 18 characters")]
    public string Cnpj { get; init; }
    [EmailAddress(ErrorMessage = "Client email is invalid")]
    [MaxLength(60, ErrorMessage = "Client email max 60 characters")]
    public string Email { get; init; }
    [MinLength(9, ErrorMessage = "Client phone min length 9 characters")]
    [MaxLength(15, ErrorMessage = "Client phone max 15 characters")]
    public string Phone { get; init; }
    public Guid ClientAvatarId { get; init; }
    public ClientAvatar Avatar { get; private set; }
    public eClientTag? Tag { get; private set; }
    [JsonIgnore]
    public ICollection<Ticket> Tickets { get; init; } = [];

    public ICollection<Attachment> Attachments { get; init; } = [];

    // EF
    #pragma warning disable
    private Client() { }

    private Client(
        string name,
        string cnpj,
        string email,
        string phone,
        eClientTag? tag
    )
    {
        Name = SpellCheckService.CapitalizeName(name);
        Cnpj = SpellCheckService.CapitalizeName(cnpj);
        Phone = SpellCheckService.CleanSpecialChar(phone);
        Email = email;
        Tag = tag;
        Tickets = [];
    }

    public void SetAvatar(ClientAvatar avatar)
    {
        this.Avatar = avatar;
    }
    
    public static class Factory
    {
        public static Client Create(
            string name,
            string cnpj,
            string email,
            string phone,
            eClientTag? tag
        )
        {
            return new Client(
                name, cnpj, email, phone, tag
            );
        }
    }
}


