namespace Banks.Exceptions;

public class InvalidDepositInterestRateSelectorException : BanksDomainException
{
    private InvalidDepositInterestRateSelectorException(string? message)
        : base(message) { }

    public static InvalidDepositInterestRateSelectorException OnGapsIntersection()
        => new InvalidDepositInterestRateSelectorException($"Money gaps must not intersect");

    public static InvalidDepositInterestRateSelectorException OnEmptyGapsList()
        => new InvalidDepositInterestRateSelectorException($"There must be at least one money gap");
}