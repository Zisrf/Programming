using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Models;

namespace Reports.DataAccess.Configurations.Models;

public class HandlingInfoConfiguration : IEntityTypeConfiguration<HandlingInfo>
{
    public void Configure(EntityTypeBuilder<HandlingInfo> builder)
    {
        builder.HasKey(x => x.EmployeeId);
    }
}
