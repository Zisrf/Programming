using Isu.Models;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(Group group, StudentName fullName)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(fullName);

        Id = StudentId.NewId();
        Group = group;
        FullName = fullName;

        group.AddStudent(this);
    }

    public StudentId Id { get; }
    public Group Group { get; private set; }
    public StudentName FullName { get; set; }

    public void ChangeGroup(Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(newGroup);

        newGroup.AddStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Student);

    public bool Equals(Student? other)
        => Id.Equals(other?.Id);

    public override int GetHashCode()
        => Id.GetHashCode();
}