using Microsoft.EntityFrameworkCore;
using Reports.DataAccess;

namespace Reports.Tests.Application;

public abstract class ApplicationTest : IDisposable
{
    protected readonly ReportsDatabaseContext Context;

    public ApplicationTest()
    {
        var optionBuilder = new DbContextOptionsBuilder<ReportsDatabaseContext>();

        DbContextOptions<ReportsDatabaseContext> options =
            optionBuilder.UseSqlite($"Data Source={Guid.NewGuid()}.db").Options;

        Context = new ReportsDatabaseContext(options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();

        GC.SuppressFinalize(this);
    }
}