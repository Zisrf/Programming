using Isu.Exceptions;

namespace Isu.Models;

public record StudentName
{
    public StudentName(string firstName, string secondName, string? patronymicName = null)
    {
        InvalidStudentNameException.ThrowIfNameEmptyOrWhiteSpace(firstName);
        InvalidStudentNameException.ThrowIfNameEmptyOrWhiteSpace(secondName);
        InvalidStudentNameException.ThrowIfNameEmptyOrWhiteSpace(patronymicName);

        FirstName = firstName;
        SecondName = secondName;
        PatronymicName = patronymicName;
    }

    public StudentName(string fullName)
    {
        InvalidStudentNameException.ThrowIfNameEmptyOrWhiteSpace(fullName);

        string[] names = fullName.Split(' ');
        if (names.Length is < 2 or > 3)
            throw new InvalidStudentNameException($"{fullName} is invalid student full name");

        FirstName = names[0];
        SecondName = names[1];
        PatronymicName = names.Length > 2 ? names[2] : null;
    }

    public string FirstName { get; }
    public string SecondName { get; }
    public string? PatronymicName { get; }

    public override string ToString() => PatronymicName switch
    {
        null => $"{FirstName} {SecondName}",
        _ => $"{FirstName} {SecondName} {PatronymicName}"
    };
}
