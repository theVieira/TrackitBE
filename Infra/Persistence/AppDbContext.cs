using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Ticket> Tickets { get; init; }
    public required DbSet<Client> Clients { get; init; }
    public required DbSet<Tech> Techs { get; init; }
    public required DbSet<TextAction> TextActions { get; init; }
    public required DbSet<TimeAction> TimeActions { get; init; }
    public required DbSet<Avatar> Avatars { get; init; }
    public required DbSet<Attachment> Attachments { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.BaseType != null) continue; 

            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType)) continue;
            
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, "IsDeleted");
            var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}