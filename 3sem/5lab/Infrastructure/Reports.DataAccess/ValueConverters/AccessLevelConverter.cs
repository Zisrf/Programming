using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reports.Core.ValueObjects;

namespace Reports.DataAccess.ValueConverters;

public class AccessLevelConverter : ValueConverter<AccessLevel, int>
{
    public AccessLevelConverter()
        : base(x => x.Value, x => new AccessLevel(x)) { }
}