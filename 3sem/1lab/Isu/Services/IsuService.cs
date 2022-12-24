using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Student> _students;
    private readonly List<Group> _groups;

    public IsuService()
    {
        _students = new List<Student>();
        _groups = new List<Group>();
    }

    public IReadOnlyList<Student> Students => _students;
    public IReadOnlyList<Group> Groups => _groups;

    public Group AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (_groups.Any(g => g.Name.Equals(name)))
            throw InvalidIsuOperationException.OnAddExistingGroup(name);

        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, StudentName fullName)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(fullName);

        var newStudent = new Student(group, fullName);
        _students.Add(newStudent);
        return newStudent;
    }

    public Student GetStudent(StudentId id)
    {
        ArgumentNullException.ThrowIfNull(id);

        return FindStudent(id)
            ?? throw InvalidIsuOperationException.OnGetNonExistentStudent(id);
    }

    public Student? FindStudent(StudentId id)
    {
        ArgumentNullException.ThrowIfNull(id);

        return _students.Find(s => s.Id.Equals(id));
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);

        return _groups
            .Where(g => g.Name.Equals(groupName))
            .SelectMany(g => g.Students)
            .ToList();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        ArgumentNullException.ThrowIfNull(courseNumber);

        return _groups
            .Where(g => g.Name.CourseNumber.Equals(courseNumber))
            .SelectMany(g => g.Students)
            .ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName);

        return _groups.Find(g => g.Name.Equals(groupName));
    }

    public IReadOnlyList<Group> FindGroups(CourseNumber courseNumber)
    {
        ArgumentNullException.ThrowIfNull(courseNumber);

        return _groups.FindAll(g => g.Name.CourseNumber.Equals(courseNumber));
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(newGroup);

        student.ChangeGroup(newGroup);
    }
}