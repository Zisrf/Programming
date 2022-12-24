namespace Banks.Exceptions;

public class InvalidPassportException : BanksDomainException
{
    private InvalidPassportException(string? message)
        : base(message) { }

    public static InvalidPassportException OnInvalidNumber(int number)
        => new InvalidPassportException($"Passport number ({number}) must have 4 digits");

    public static InvalidPassportException OnInvalidSeries(int series)
        => new InvalidPassportException($"Passport number ({series}) must have 6 digits");
}