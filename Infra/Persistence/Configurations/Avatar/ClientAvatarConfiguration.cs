using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class ClientAvatarConfiguration :  IEntityTypeConfiguration<ClientAvatar>
{
    public void Configure(EntityTypeBuilder<ClientAvatar> builder)
    {
        builder
            .HasOne<Client>(x => x.Client)
            .WithOne(x => x.Avatar)
            .HasForeignKey<ClientAvatar>(x => x.ClientId)
            .IsRequired(false);
    }
}