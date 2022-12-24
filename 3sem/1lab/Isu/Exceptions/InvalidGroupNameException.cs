namespace Isu.Exceptions;

public class InvalidGroupNameException : IsuDomainException
{
    public InvalidGroupNameException(string groupName)
        : base($"{groupName} is invalid group name")
    { }
}