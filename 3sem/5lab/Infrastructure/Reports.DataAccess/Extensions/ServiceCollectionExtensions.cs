using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reports.Abstractions.DataAccess;

namespace Reports.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> configuration)
    {
        collection.AddDbContext<ReportsDatabaseContext>(configuration);
        collection.AddScoped<IReportsDatabaseContext>(x => x.GetRequiredService<ReportsDatabaseContext>());

        return collection;
    }
}