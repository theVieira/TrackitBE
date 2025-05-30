using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class TechAvatarConfiguration :  IEntityTypeConfiguration<TechAvatar>
{
    public void Configure(EntityTypeBuilder<TechAvatar> builder)
    {
        builder
            .HasOne<Tech>(x => x.Tech)
            .WithOne(x => x.Avatar)
            .HasForeignKey<TechAvatar>(x => x.TechId)
            .IsRequired(false);
    }
}