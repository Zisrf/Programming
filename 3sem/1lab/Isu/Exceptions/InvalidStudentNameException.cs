namespace Isu.Exceptions;

public class InvalidStudentNameException : IsuDomainException
{
    public InvalidStudentNameException(string name, string namePart)
        : base($"{name} is invalid student {namePart}")
    { }
}
