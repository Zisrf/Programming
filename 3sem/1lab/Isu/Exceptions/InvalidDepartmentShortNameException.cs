namespace Isu.Exceptions;

public class InvalidDepartmentShortNameException : IsuDomainException
{
    public InvalidDepartmentShortNameException(char departmentShortName)
        : base($"{departmentShortName} is invalid department short name")
    { }
}
