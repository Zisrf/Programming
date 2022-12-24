namespace Banks.Exceptions;

public class InvalidBankAccountOperationException : BanksDomainException
{
    private InvalidBankAccountOperationException(string? message)
        : base(message) { }

    public static InvalidBankAccountOperationException OnInvalidMoneyAmount()
        => new InvalidBankAccountOperationException("Money amount must be positive");

    public static InvalidBankAccountOperationException OnInvalidInterestRate()
        => new InvalidBankAccountOperationException($"Interest rate must be in [0; 1]");

    public static InvalidBankAccountOperationException OnInvalidCreditLimit()
        => new InvalidBankAccountOperationException($"Credit limit must be positive");

    public static InvalidBankAccountOperationException OnInvalidCommission()
        => new InvalidBankAccountOperationException($"Commission must be positive");

    public static InvalidBankAccountOperationException OnShortageOfMoney(decimal sum, decimal balance)
        => new InvalidBankAccountOperationException($"Unable to withdraw {sum}, account has only {balance}");
}