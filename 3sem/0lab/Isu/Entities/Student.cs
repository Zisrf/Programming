using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(int id, Group group, string fullName)
        : this(id, group, new StudentName(fullName))
    { }

    public Student(int id, Group group, StudentName fullName)
    {
        Id = id;
        Group = group;
        FullName = fullName;
    }

    public int Id { get; }
    public Group Group { get; set; }
    public StudentName FullName { get; set; }
}