using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Employees;

namespace Reports.DataAccess.Configurations.Employees;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.Navigation(x => x.Subordinates).HasField("_subordinates");
    }
}
