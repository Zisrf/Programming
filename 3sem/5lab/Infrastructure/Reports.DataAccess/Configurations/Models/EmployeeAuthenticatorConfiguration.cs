using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Models;

namespace Reports.DataAccess.Configurations.Models;

public class EmployeeAuthenticatorConfiguration : IEntityTypeConfiguration<Authenticator>
{
    public void Configure(EntityTypeBuilder<Authenticator> builder)
    {
        builder.HasKey(x => x.Login);
    }
}
