using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class TextActionConfiguration : IEntityTypeConfiguration<TextAction>
{
    public void Configure(EntityTypeBuilder<TextAction> builder)
    {
        builder.ToTable("TextActions");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Content).IsRequired();
        
        builder.Property(x => x.Content).HasMaxLength(500);
        
        builder
            .Property(x => x.Type)
            .HasConversion<string>();

        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<TextAction>(TextActionType.Default)
            .HasValue<Note>(TextActionType.Note)
            .HasValue<Feedback>(TextActionType.Feedback);
    }
}