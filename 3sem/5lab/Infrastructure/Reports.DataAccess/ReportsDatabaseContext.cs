using Microsoft.EntityFrameworkCore;
using Reports.Abstractions.DataAccess;
using Reports.Core.Employees;
using Reports.Core.Entities;
using Reports.Core.Messages;
using Reports.Core.MessageSources;
using Reports.Core.Models;
using Reports.Core.ValueObjects;
using Reports.DataAccess.ValueConverters;

namespace Reports.DataAccess;

public class ReportsDatabaseContext : DbContext, IReportsDatabaseContext
{
    public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Report> Reports { get; protected init; } = null!;
    public DbSet<Account> Accounts { get; protected init; } = null!;
    public DbSet<Worker> Workers { get; protected init; } = null!;
    public DbSet<Manager> Managers { get; protected init; } = null!;
    public DbSet<Message> Messages { get; protected init; } = null!;
    public DbSet<MessageSource> Sources { get; protected init; } = null!;
    public DbSet<Authenticator> Authenticators { get; protected init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<AccessLevel>().HaveConversion<AccessLevelConverter>();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}