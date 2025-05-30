using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class TechConfiguration : IEntityTypeConfiguration<Tech>
{
    public void Configure(EntityTypeBuilder<Tech> builder)
    {
        builder.ToTable("Techs");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.Role).IsRequired();

        builder.Property(x => x.Password).HasMaxLength(300);
        builder.Property(x => x.Email).HasMaxLength(60);
        builder.Property(x => x.Name).HasMaxLength(50);
        
        builder.Property(x => x.Role).HasConversion<string>();
        
        builder.HasIndex(x => x.Phone).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        
        builder
            .HasOne<TechAvatar>(x => x.Avatar)
            .WithOne(x  => x.Tech)
            .HasForeignKey<TechAvatar>(x => x.TechId);
        
        builder
            .HasMany<Ticket>(x => x.Tickets)
            .WithOne(x => x.CreatedBy)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}