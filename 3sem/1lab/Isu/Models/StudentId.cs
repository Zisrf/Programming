using Isu.Exceptions;

namespace Isu.Models;

public record StudentId
{
    public const int MinValue = 100000;
    public const int MaxValue = 999999;

    private static int _nextId = MinValue;

    public StudentId(int id)
    {
        if (id is < MinValue or > MaxValue)
            throw new StudentIdIsOutOfRangeException(id);

        Id = id;
    }

    public int Id { get; }

    public static StudentId NewId()
        => new StudentId(_nextId++);

    public override string ToString()
        => Id.ToString();
}
