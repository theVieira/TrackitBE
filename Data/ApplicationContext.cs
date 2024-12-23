using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Trackit.Core.Entities;

namespace Trackit.Infra.Data;

public class ApplicationContext : DbContext
{
  private readonly string connectionString = Env.GetString("CONNECTION_STRING");
  public required DbSet<Ticket> Tickets { get; set; }
  public required DbSet<Client> Clients { get; set; }
  public required DbSet<Tech> Techs { get; set; }
  public required DbSet<Attachment> Attachments { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql(connectionString);
    base.OnConfiguring(optionsBuilder);
  }
}