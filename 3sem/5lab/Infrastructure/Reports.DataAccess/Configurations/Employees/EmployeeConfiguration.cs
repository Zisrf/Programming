using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Employees;

namespace Reports.DataAccess.Configurations.Employees;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasDiscriminator()
            .HasValue<Worker>(nameof(Worker))
            .HasValue<Manager>(nameof(Manager));

        builder.Ignore(x => x.HandlingInfo);
    }
}
