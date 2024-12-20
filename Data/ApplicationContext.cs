using Microsoft.EntityFrameworkCore;
using Trackit.Core.Entities;

namespace Trackit.Infra.Data;

public class ApplicationContext : DbContext
{
  public required DbSet<Ticket> Tickets { get; set; }
  public required DbSet<Client> Clients { get; set; }
  public required DbSet<Tech> Techs { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
    optionsBuilder.UseNpgsql(connectionString);
    base.OnConfiguring(optionsBuilder);
  }
}