using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");

        builder.HasKey(x=> x.Id);
        builder.Property(x=> x.Id).ValueGeneratedNever();

        builder.Property(x => x.Filename).IsRequired();
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.Path).IsRequired();
        builder.Property(x => x.Url).IsRequired();
        
        builder
            .HasDiscriminator<AttachmentType>("Type")
            .HasValue<ClientAttachment>(AttachmentType.Client)
            .HasValue<TicketAttachment>(AttachmentType.Ticket);
    }
}