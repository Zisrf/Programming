namespace Reports.Domain.Common.Exceptions;

public abstract class ReportsDomainException : Exception
{
    protected ReportsDomainException(string? message)
        : base(message) { }
}