using System.ComponentModel.DataAnnotations;
using Trackit.Core.Entities;

namespace Trackit.Infra.DTO;

public class CreateTechDTO
{
  [Required]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string Login { get; set; } = string.Empty;
  [Required]
  public string Password { get; set; } = string.Empty;
  [Required]
  public string Phone { get; set; } = string.Empty;
  [Required]
  public TechRole Role { get; set; }
  [EmailAddress]
  public string Email { get; set; } = string.Empty;
}