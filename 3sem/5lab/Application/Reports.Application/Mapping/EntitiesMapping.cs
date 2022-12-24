using Reports.Application.Dto.Entities;
using Reports.Core.Entities;

namespace Reports.Application.Mapping;

public static class EntitiesMapping
{
    public static AccountDto AsDto(this Account account)
    {
        return new AccountDto(account.Id, account.AccessLevel.Value);
    }

    public static ReportDto AsDto(this Report report)
    {
        return new ReportDto(report.Id, report.Date);
    }
}