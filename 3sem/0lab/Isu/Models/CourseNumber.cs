using Isu.Exceptions;

namespace Isu.Models;

public record CourseNumber
{
    public CourseNumber(char type, char number)
        : this((DegreeType)(type - '0'), number - '0')
    { }

    public CourseNumber(DegreeType type, int number)
    {
        if (!IsCorrectCourseNumber(type, number))
            throw new InvalidCourseNumberException($"{(int)type}{number} is invalid course number");

        Type = type;
        Number = number;
    }

    public DegreeType Type { get; }
    public int Number { get; }

    public override string ToString() => $"{(int)Type}{Number}";

    private static bool IsCorrectCourseNumber(DegreeType type, int number) => type switch
    {
        DegreeType.Bachelor => number is >= 1 and <= 4,
        DegreeType.Master => number is >= 1 and <= 2,
        _ => false
    };
}