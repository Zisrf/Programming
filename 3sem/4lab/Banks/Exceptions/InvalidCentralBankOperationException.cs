namespace Banks.Exceptions;

public class InvalidCentralBankOperationException : BanksDomainException
{
    private InvalidCentralBankOperationException(string? message)
        : base(message) { }

    public static InvalidCentralBankOperationException OnGetNonExistentBank(Guid bankId)
        => new InvalidCentralBankOperationException($"Central bank hasn't bank {bankId}");

    public static InvalidCentralBankOperationException OnGetNonExistentTransaction(Guid transactionId)
        => new InvalidCentralBankOperationException($"Central bank hasn't transaction {transactionId}");
}