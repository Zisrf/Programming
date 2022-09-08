using Isu.Exceptions;

namespace Isu.Models;

public record GroupNumber
{
    public GroupNumber(string number)
    {
        if (number.Length > 3 || number.Any(c => c is < '0' or > '9'))
            throw new InvalidGroupNumberException($"{number} is invalid course number");

        Number = int.Parse(number);
    }

    public GroupNumber(int number)
    {
        if (number is < 0 or > 999)
            throw new InvalidGroupNumberException($"{number} is invalid course number");

        Number = number;
    }

    public int Number { get; }

    public override string ToString() => Number.ToString();
}
