namespace Reports.Application.Common.Exceptions;

public abstract class ReportsApplicationException : Exception
{
    protected ReportsApplicationException(string? message)
        : base(message) { }
}