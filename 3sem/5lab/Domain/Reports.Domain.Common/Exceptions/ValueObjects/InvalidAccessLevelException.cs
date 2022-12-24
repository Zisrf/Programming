using Reports.Domain.Common.Exceptions;

namespace Reports.Domain.Common.Exceptions.ValueObjects;

public class InvalidAccessLevelException : ReportsDomainException
{
    private InvalidAccessLevelException(string? message)
        : base(message) { }

    public static InvalidAccessLevelException OnNegativeValue()
    {
        return new InvalidAccessLevelException($"Access level mustn't be negative");
    }
}