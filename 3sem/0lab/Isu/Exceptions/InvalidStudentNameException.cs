namespace Isu.Exceptions;

public class InvalidStudentNameException : IsuDomainException
{
    public InvalidStudentNameException(string? message)
        : base(message)
    { }

    public static void ThrowIfNameEmptyOrWhiteSpace(string? name)
    {
        if (name is not null && string.IsNullOrWhiteSpace(name))
            throw new InvalidStudentNameException("Inavlid empty name");
    }
}
