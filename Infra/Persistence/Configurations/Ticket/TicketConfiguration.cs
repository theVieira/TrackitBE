using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trackit.Domain.Entities;

namespace Trackit.Infra.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(x => x.Category).HasConversion<string>();
        builder.Property(x => x.Priority).HasConversion<string>();
        builder.Property(x => x.Status).HasConversion<string>();
        builder.Property(x => x.Tag).HasConversion<string>();
        
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Tag).IsRequired();
        builder.Property(x => x.Category).IsRequired();
        builder.Property(x => x.Tag).IsRequired();
        builder.Property(x => x.Tag).IsRequired();
        
        builder.Property(x => x.Description).HasMaxLength(600);
        
        builder
            .HasOne<Client>(x => x.Client)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne<Tech>(x => x.CreatedBy)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasMany<Note>(x => x.Notes)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany<Feedback>(x => x.Feedbacks)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany<Progress>(x => x.Progress)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany<Finish>(x => x.Finish)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany<Reopen>(x => x.Reopen)
            .WithOne(x => x.Ticket)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}