namespace Isu.Exceptions;

public class InvalidGroupNameException : IsuDomainException
{
    public InvalidGroupNameException(string? message)
        : base(message)
    { }
}