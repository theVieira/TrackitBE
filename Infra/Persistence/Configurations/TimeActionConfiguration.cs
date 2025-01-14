using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class TimeActionConfiguration : IEntityTypeConfiguration<TimeAction>
{
    public void Configure(EntityTypeBuilder<TimeAction> builder)
    {
        builder.ToTable("TimeActions");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder
            .Property(x => x.Type)
            .HasConversion<string>();
        
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<TimeAction>(TimeActionType.Default)
            .HasValue<Progress>(TimeActionType.Progress)
            .HasValue<Finish>(TimeActionType.Finish)
            .HasValue<Reopen>(TimeActionType.Reopen);
    }
}