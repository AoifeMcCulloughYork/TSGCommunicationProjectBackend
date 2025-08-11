using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TSGCommunicationProjectBackend.Data.Entities;
namespace TSGCommunicationProjectBackend.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Communication> Communications { get; set; }
    public DbSet<CommunicationStatusHistory> CommunicationStatusHistories { get; set; }
    public DbSet<CommunicationType> CommunicationTypes { get; set; }
    public DbSet<CommunicationTypeStatus> CommunicationTypeStatuses { get; set; }
    public DbSet<Member> Members { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
