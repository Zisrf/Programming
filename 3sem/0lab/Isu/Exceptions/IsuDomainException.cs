namespace Isu.Exceptions;

public class IsuDomainException : Exception
{
    public IsuDomainException(string? message)
        : base(message)
    { }
}