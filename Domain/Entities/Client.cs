using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Trackit.Utils;

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
    public ClientTag? Tag { get; private set; }
    [JsonIgnore]
    public ICollection<Ticket> Tickets { get; init; }

    // EF
    private Client() { }

    private Client(
        string name,
        string cnpj,
        string email,
        string phone,
        ClientTag? tag
    )
    {
        Name = SpellCheck.CapitalizeName(name);
        Cnpj = SpellCheck.CapitalizeName(cnpj);
        Phone = SpellCheck.CleanSpecialChar(phone);
        Email = email;
        Tag = tag;
        Tickets = [];
    }

    public static class Factory
    {
        public static Client Create(
            string name,
            string cnpj,
            string email,
            string phone,
            ClientTag? tag
        )
        {
            return new Client(
                name, cnpj, email, phone, tag
            );
        }
    }
}

public enum ClientTag
{
    Vip
}