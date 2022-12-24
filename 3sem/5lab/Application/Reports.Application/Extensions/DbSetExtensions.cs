using Microsoft.EntityFrameworkCore;
using Reports.Application.Common.Exceptions.NotFound;

namespace Reports.DataAccess.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> GetEntityByIdAsync<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        T? entity = await set.FindAsync(new object[] { id }, cancellationToken);

        if (entity is null)
            throw new EntityNotFoundException<T>(id);

        return entity;
    }
}