namespace Isu.Exceptions;

public class InvalidCourseNumberException : IsuDomainException
{
    public InvalidCourseNumberException(string? message)
        : base(message)
    { }
}