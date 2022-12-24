using Isu.Models;

namespace Isu.Exceptions;

public class InvalidIsuOperationException : IsuDomainException
{
    private InvalidIsuOperationException(string? message)
        : base(message)
    { }

    public static InvalidIsuOperationException OnAddExistingGroup(GroupName groupName)
        => new InvalidIsuOperationException($"Group {groupName} is already exists");

    public static InvalidIsuOperationException OnGetNonExistentStudent(StudentId studentId)
        => new InvalidIsuOperationException($"No student with id {studentId}");
}
