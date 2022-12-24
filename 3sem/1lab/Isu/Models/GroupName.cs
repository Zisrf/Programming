using Isu.Exceptions;

namespace Isu.Models;

public record GroupName
{
    public GroupName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (name.Length is < 5 or > 6)
            throw new InvalidGroupNameException(name);

        Department = new Department(name[0]);
        CourseNumber = new CourseNumber(name[1], name[2]);
        GroupNumber = new GroupNumber(name[3..]);
    }

    public Department Department { get; }
    public CourseNumber CourseNumber { get; }
    public GroupNumber GroupNumber { get; }

    public override string ToString()
        => $"{Department}{CourseNumber}{GroupNumber}";
}