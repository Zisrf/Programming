using System.Globalization;
using Isu.Exceptions;

namespace Isu.Models;

public record CourseNumber
{
    public CourseNumber(char type, char number)
        : this((DegreeType)CharUnicodeInfo.GetDigitValue(type), CharUnicodeInfo.GetDigitValue(number))
    { }

    public CourseNumber(DegreeType type, int number)
    {
        if (!IsCorrectCourseNumber(type, number))
            throw new InvalidCourseNumberException(type, number);

        Type = type;
        Number = number;
    }

    public DegreeType Type { get; }
    public int Number { get; }

    public override string ToString()
        => $"{(int)Type}{Number}";

    private static bool IsCorrectCourseNumber(DegreeType type, int number) => type switch
    {
        DegreeType.Bachelor => number is >= 1 and <= 4,
        DegreeType.Master => number is >= 1 and <= 2,
        DegreeType.Specialist => number is >= 1 and <= 5,
        _ => false
    };
}