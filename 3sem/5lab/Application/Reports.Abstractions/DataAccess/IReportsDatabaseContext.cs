using Microsoft.EntityFrameworkCore;
using Reports.Core.Employees;
using Reports.Core.Entities;
using Reports.Core.Messages;
using Reports.Core.MessageSources;
using Reports.Core.Models;

namespace Reports.Abstractions.DataAccess;

public interface IReportsDatabaseContext
{
    DbSet<Worker> Workers { get; }
    DbSet<Manager> Managers { get; }

    DbSet<Message> Messages { get; }
    DbSet<MessageSource> Sources { get; }

    DbSet<Report> Reports { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Authenticator> Authenticators { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}