namespace Trackit.Core.Entities;

public class BaseEntity
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public DateTime CreatedAt { get; init; } = DateTime.Now;
}