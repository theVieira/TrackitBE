using System.ComponentModel.DataAnnotations;
using Trackit.Utils;

namespace Trackit.Core.Entities;

public class Client : BaseEntity
{
  public string Name { get; private set; } = string.Empty;
  public string CNPJ { get; init; } = string.Empty;
  public string Phone { get; private set; } = string.Empty;
  public bool Status { get; private set; } = true;
  [EmailAddress]
  public string? Email { get; private set; }

  public Client() {}

  public Client(string name, string cnpj, string phone, string? email)
  {
    Name = SpellCheck.CapitalizeName(name);
    CNPJ = SpellCheck.CleanSpecialChar(cnpj);
    Phone = SpellCheck.CleanSpecialChar(phone);
    Email = email;
  }

  public string SetName
  {
    set => Name = SpellCheck.CapitalizeName(value);
  }

  public string SetPhone
  {
    set => Phone = SpellCheck.CleanSpecialChar(value);
  }

  public bool SwapStatus
  {
    set => Status = !Status;
  }

  public string SetEmail
  {
    set => Email = value;
  }
}