using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Endpoints.Ticket;

public class ClientAttachmentConfiguration : IEntityTypeConfiguration<ClientAttachment>
{
    public void Configure(EntityTypeBuilder<ClientAttachment> builder)
    {
        builder
            .HasOne<Domain.Entities.Client>(x => x.Client)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.ClientId)
            .IsRequired(false);
    }
}