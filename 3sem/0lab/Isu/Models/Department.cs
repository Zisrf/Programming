using Isu.Exceptions;

namespace Isu.Models;

public record Department
{
    public Department(char name)
    {
        if (name is < 'A' or > 'Z')
            throw new InvalidDepartmentException($"{name} is invalid department name");

        Name = name;
    }

    public char Name { get; }

    public override string ToString() => Name.ToString();
}
