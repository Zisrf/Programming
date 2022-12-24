namespace Isu.Exceptions;

public abstract class IsuDomainException : Exception
{
    protected IsuDomainException(string? message)
        : base(message)
    { }
}