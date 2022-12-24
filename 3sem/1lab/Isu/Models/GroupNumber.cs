using Isu.Exceptions;

namespace Isu.Models;

public record GroupNumber
{
    public GroupNumber(string number)
    {
        ArgumentNullException.ThrowIfNull(number);

        if (number.Length is < 2 or > 3 || number.Any(c => !char.IsDigit(c)))
            throw new InvalidGroupNumberException(number);

        Number = number;
    }

    public string Number { get; }

    public override string ToString()
        => Number;
}
