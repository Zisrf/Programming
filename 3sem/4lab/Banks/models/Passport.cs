using Banks.Exceptions;

namespace Banks.Models;

public readonly record struct Passport
{
    public Passport(int number, int series)
    {
        if (number is < 1000 or > 9999)
            throw InvalidPassportException.OnInvalidNumber(number);

        if (series is < 100000 or > 999999)
            throw InvalidPassportException.OnInvalidSeries(series);

        Number = number;
        Series = series;
    }

    public int Number { get; }
    public int Series { get; }

    public override string ToString()
    {
        return $"{Number}{Series}";
    }
}