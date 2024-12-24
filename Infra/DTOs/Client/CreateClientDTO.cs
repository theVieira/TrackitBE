
using System.ComponentModel.DataAnnotations;

namespace Trackit.Infra.DTO;

public class CreateClientDTO
{
  [Required]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string Cnpj { get; set; } = string.Empty;
  [Required]
  public string Phone { get; set; } = string.Empty;
  [EmailAddress]
  public string? Email { get; set; } = string.Empty;
}