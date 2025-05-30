using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Application.Services;

namespace Trackit.Domain.Entities;

public class Tech : BaseEntity
{
    [MinLength(3, ErrorMessage = "Tech name min length is 3")]
    [MaxLength(50, ErrorMessage = "Tech name max length is 50")]
    public string Name { get; private set; }
    
    [MinLength(6, ErrorMessage = "Tech password min length is 6")]
    [MaxLength(40, ErrorMessage = "Tech password max length is 40")]
    [JsonIgnore]
    public string Password { get; private set; }
    
    [MinLength(9, ErrorMessage = "Tech phone min length is 9")]
    [MaxLength(15, ErrorMessage = "Tech phone max length is 15")]
    public string Phone { get; private set; }
    
    [EmailAddress(ErrorMessage = "Tech email address is invalid")]
    [MaxLength(60, ErrorMessage = "Tech email max length is 60")]
    public string Email { get; init; }
    [Required]
    public eTechRole Role { get; private set; }
    
    [JsonIgnore]
    public ICollection<Ticket> Tickets { get; init; }
    public TechAvatar? Avatar { get; private set; }

    // EF
    #pragma warning disable
    private Tech() { }

    public void SetAvatar(TechAvatar avatar)
    {
        this.Avatar = avatar;
    }

    public bool CheckPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, Password);
    }

    private Tech(
        string name, 
        string password, 
        string phone, 
        string email,
        eTechRole role
    )
    {
        Name = SpellCheckService.CapitalizeName(name);
        Email = email;
        Phone = SpellCheckService.CleanSpecialChar(phone);
        Role = role;
        Tickets = [];
        
        var salt = BCrypt.Net.BCrypt.GenerateSalt(8);
        Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public static class Factory
    {
        public static Tech Create(
            string name,
            string password,
            string phone,
            string email,
            eTechRole role
        )
        {
            return new Tech(
                name, password, phone, email, role
            );
        }
    }
}

