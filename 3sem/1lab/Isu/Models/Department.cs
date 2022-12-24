using Isu.Exceptions;

namespace Isu.Models;

public record Department
{
    public Department(char shortName)
    {
        if (!char.IsUpper(shortName))
            throw new InvalidDepartmentShortNameException(shortName);

        ShortName = shortName;
    }

    public char ShortName { get; }

    public override string ToString()
        => ShortName.ToString();
}
