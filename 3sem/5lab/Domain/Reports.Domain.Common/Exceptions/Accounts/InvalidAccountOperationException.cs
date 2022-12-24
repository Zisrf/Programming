using Reports.Domain.Common.Exceptions;

namespace Reports.Domain.Common.Exceptions.Accounts;

public class InvalidAccountOperationException : ReportsDomainException
{
    private InvalidAccountOperationException(string? message)
        : base(message) { }

    public static InvalidAccountOperationException OnAddExistingMessageSource(Guid accountId, Guid sourceId)
    {
        return new InvalidAccountOperationException($"Account {accountId} already has message source {sourceId}");
    }
}
