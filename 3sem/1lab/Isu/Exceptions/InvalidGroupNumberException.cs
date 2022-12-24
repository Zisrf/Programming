namespace Isu.Exceptions;

public class InvalidGroupNumberException : IsuDomainException
{
    public InvalidGroupNumberException(string groupNumber)
        : base($"{groupNumber} is invalid course number")
    { }
}
