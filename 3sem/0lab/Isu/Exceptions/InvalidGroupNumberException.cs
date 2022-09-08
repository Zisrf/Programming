namespace Isu.Exceptions;

public class InvalidGroupNumberException : IsuDomainException
{
    public InvalidGroupNumberException(string? message)
        : base(message)
    { }
}
