using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    public const int MaxStudentsCount = 30;

    private readonly List<Student> _students;

    public Group(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        _students = new List<Student>();
        Name = name;
    }

    public GroupName Name { get; }
    public IReadOnlyList<Student> Students => _students;

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (_students.Contains(student))
            throw InvalidGroupOperationException.OnAddExistingStudent(student, this);

        if (_students.Count == MaxStudentsCount)
            throw InvalidGroupOperationException.OnAddStudentToFullGroup(student, this);

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (!_students.Remove(student))
            throw InvalidGroupOperationException.OnRemoveNonExistentStudent(student, this);
    }

    public override bool Equals(object? obj)
        => Equals(obj as Group);

    public bool Equals(Group? other)
        => Name.Equals(other?.Name);

    public override int GetHashCode()
        => Name.GetHashCode();
}