using Isu.Models;

namespace Isu.Exceptions;

public class StudentIdIsOutOfRangeException : IsuDomainException
{
    public StudentIdIsOutOfRangeException(int id)
        : base($"Id {id} is out of range [{StudentId.MinValue};{StudentId.MaxValue}]")
    { }
}
