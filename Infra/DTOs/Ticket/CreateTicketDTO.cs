using System.ComponentModel.DataAnnotations;
using Trackit.Core.Entities;

public class CreateTicketDTO
{
  [Required]
  public string ClientId { get; set; } = string.Empty;
  [Required]
  public string TechId { get; set; } = string.Empty;
  [Required]
  public TicketCategory Category { get; set; }
  [Required]
  public TicketPriority Priority { get; set; }
  [Required]
  public string Description { get; set; } = string.Empty;
}