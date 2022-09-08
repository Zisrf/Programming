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
        Group newGroup = new(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string fullName)
    {
        Student newStudent = new(_students.Count, group, fullName);
        _students.Add(newStudent);
        group.AddStudent(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new IsuDomainException($"No student with id {id}");
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(s => s.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.FindAll(s => s.Group.Name.Equals(groupName));
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.FindAll(s => s.Group.CourseNumber.Equals(courseNumber));
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(g => g.Name.Equals(groupName));
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.FindAll(g => g.CourseNumber.Equals(courseNumber));
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.Group.RemoveStudent(student);
        newGroup.AddStudent(student);
        student.Group = newGroup;
    }
}