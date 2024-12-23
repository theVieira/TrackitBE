using System.ComponentModel.DataAnnotations;

namespace Trackit.Core.Entities;

public class BaseEntity
{
  [Key]
  public Guid Id { get; init; } = Guid.NewGuid();
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}