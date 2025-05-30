using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
{
    public void Configure(EntityTypeBuilder<Avatar> builder)
    {
        builder.ToTable("Avatars");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property<AvatarType>(c => c.Type).HasConversion<string>();
        
        builder.Property(x => x.Filename).IsRequired();
        builder.Property(x => x.Path).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        
        builder
            .HasDiscriminator<AvatarType>("Type")
            .HasValue<Avatar>(AvatarType.Default)
            .HasValue<ClientAvatar>(AvatarType.Client)
            .HasValue<TechAvatar>(AvatarType.Tech);
    }
}