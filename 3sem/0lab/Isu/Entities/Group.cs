using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudentsCount = 30;

    private readonly List<Student> _students;

    public Group(string name)
        : this(new GroupName(name))
    { }

    public Group(GroupName name)
    {
        _students = new List<Student>();
        Name = name;
    }

    public GroupName Name { get; set; }

    public Department Department => Name.Department;
    public CourseNumber CourseNumber => Name.CourseNumber;
    public GroupNumber GroupNumber => Name.GroupNumber;
    public IReadOnlyList<Student> Students => _students;

    public void AddStudent(Student student)
    {
        if (_students.Count == MaxStudentsCount)
            throw new GroupOperationException($"Unable to add student {student.Id}, group {Name} is full");

        if (_students.Contains(student))
            throw new GroupOperationException($"Student {student.Id} is already in group {Name}");

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
            throw new GroupOperationException($"Student {student.Id} is not in group {Name}");

        _students.Remove(student);
    }
}