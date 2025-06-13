using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trackit.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public string SmallId { get; init; }
    public DateTime CreatedAt { get; protected init; }
    [JsonIgnore] public Boolean IsDeleted { get; private set; }
    [JsonIgnore] public DateTime? DeletedAt { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        SmallId = Id.ToString()[..8];
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    protected internal void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}