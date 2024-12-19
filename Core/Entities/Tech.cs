using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trackit.Utils;

namespace Trackit.Core.Entities;

public class Tech : BaseEntity
{
  private readonly string Login = string.Empty;
  private string Password  = string.Empty;
  public string Name { get; private set; } = string.Empty;
  public string Phone { get; private set; } = string.Empty;
  [EmailAddress]
  public string? Email { get; private set; } = string.Empty;
  [EnumDataType(typeof(TechRole))]
  public TechRole Role { get; private set; } = TechRole.User;
  
  public bool Status { get; private set; } = true;

  public Tech() {}

  public Tech(string login, string name, string password, string phone, TechRole role, string? email)
  {
    Login = login;
    Name = SpellCheck.CapitalizeName(name);
    Phone = SpellCheck.CleanSpecialChar(phone);
    Role = role;
    Email = email;
    var salt = BCrypt.Net.BCrypt.GenerateSalt();
    Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
  }

  public string SetName
  {
    set => Name = SpellCheck.CapitalizeName(value);
  }

  public string SetPhone
  {
    set => Phone = SpellCheck.CleanSpecialChar(value);
  }

  public string SetEmail
  {
    set => Email = value;
  }

  public TechRole SetRole
  {
    set => Role = value;
  }

  public bool SwapStatus
  {
    set => Status = !Status;
  }

  public string SetPassword
  {
    set
    {
      var salt = BCrypt.Net.BCrypt.GenerateSalt();
      Password = BCrypt.Net.BCrypt.HashPassword(value, salt);
    }
  }

  public bool Verify(string login, string password)
  {
    if(Login == login && BCrypt.Net.BCrypt.Verify(password, Password))
      return true;

    return false;
  }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TechRole
{
  Admin,
  User,
  Tech,
  Manager
}