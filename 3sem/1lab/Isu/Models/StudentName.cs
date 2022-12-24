using Isu.Exceptions;

namespace Isu.Models;

public record StudentName
{
    public StudentName(string firstName, string secondName, string? patronymicName = null)
    {
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(secondName);

        if (!IsCorrectName(firstName))
            throw new InvalidStudentNameException(firstName, nameof(firstName));

        if (!IsCorrectName(secondName))
            throw new InvalidStudentNameException(secondName, nameof(secondName));

        if (patronymicName is not null && !IsCorrectName(patronymicName))
            throw new InvalidStudentNameException(patronymicName, nameof(patronymicName));

        FirstName = firstName;
        SecondName = secondName;
        PatronymicName = patronymicName;
    }

    public string FirstName { get; }
    public string SecondName { get; }
    public string? PatronymicName { get; }

    public static StudentName Parse(string fullName)
    {
        ArgumentNullException.ThrowIfNull(fullName);

        string[] names = fullName.Split(' ');

        if (names.Length == 2)
            return new StudentName(names[0], names[1]);

        if (names.Length == 3)
            return new StudentName(names[0], names[1], names[2]);

        throw new InvalidStudentNameException(fullName, nameof(fullName));
    }

    public override string ToString() => PatronymicName switch
    {
        null => $"{FirstName} {SecondName}",
        _ => $"{FirstName} {SecondName} {PatronymicName}"
    };

    private static bool IsCorrectName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return name.Length > 1
            && char.IsUpper(name[0])
            && name[1..].All(c => char.IsLower(c));
    }
}