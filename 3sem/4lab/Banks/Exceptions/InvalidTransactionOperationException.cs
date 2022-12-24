namespace Banks.Exceptions;

public class InvalidTransactionOperationException : BanksDomainException
{
    private InvalidTransactionOperationException(string? message)
        : base(message) { }

    public static InvalidTransactionOperationException OnTransactionToSameAccount()
        => new InvalidTransactionOperationException($"Unable to make transaction to the same bank account");

    public static InvalidTransactionOperationException OnIncorrectExecuting()
        => new InvalidTransactionOperationException($"Executed can be only transactions with status 'Created'");

    public static InvalidTransactionOperationException OnIncorrectCanceling()
        => new InvalidTransactionOperationException($"Canceled can be only transactions with status 'Executed'");

    public static InvalidTransactionOperationException OnInvalidTransaction()
        => new InvalidTransactionOperationException($"Unable to make or cancel transaction");
}