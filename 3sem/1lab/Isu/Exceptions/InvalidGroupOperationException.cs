using Isu.Entities;

namespace Isu.Exceptions;

public class InvalidGroupOperationException : IsuDomainException
{
    private InvalidGroupOperationException(string? message)
        : base(message)
    { }

    public static InvalidGroupOperationException OnAddStudentToFullGroup(Student student, Group group)
        => new InvalidGroupOperationException($"Unable to add student {student.Id}, group {group.Name} is full");

    public static InvalidGroupOperationException OnAddExistingStudent(Student student, Group group)
        => new InvalidGroupOperationException($"Student {student.Id} is already in group {group.Name}");

    public static InvalidGroupOperationException OnRemoveNonExistentStudent(Student student, Group group)
        => new InvalidGroupOperationException($"Student {student.Id} is not in group {group.Name}");
}
