namespace Isu.Exceptions;

public class InvalidDepartmentException : IsuDomainException
{
    public InvalidDepartmentException(string? message)
        : base(message)
    { }
}
