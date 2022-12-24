namespace Banks.Exceptions;

public class InvalidBankOperationException : BanksDomainException
{
    private InvalidBankOperationException(string? message)
        : base(message) { }

    public static InvalidBankOperationException OnAddExistingClient()
        => new InvalidBankOperationException($"Bank already has this client");

    public static InvalidBankOperationException OnAddAccountToUnregisteredClient()
        => new InvalidBankOperationException($"Unable to create bank account to unregistered account");
}