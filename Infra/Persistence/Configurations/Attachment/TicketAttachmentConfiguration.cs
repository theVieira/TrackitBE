using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Endpoints.Ticket;

public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
{
    public void Configure(EntityTypeBuilder<TicketAttachment> builder)
    {
        builder
            .HasOne<Domain.Entities.Ticket>(x => x.Ticket)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.TicketId)
            .IsRequired(false);
    }
}