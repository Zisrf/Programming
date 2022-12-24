using Isu.Models;

namespace Isu.Exceptions;

public class InvalidCourseNumberException : IsuDomainException
{
    public InvalidCourseNumberException(DegreeType degreeType, int courseNumber)
        : base($"{(int)degreeType}{courseNumber} is invalid course number")
    { }
}