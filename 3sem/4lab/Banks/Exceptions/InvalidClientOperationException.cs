namespace Banks.Exceptions;

public class InvalidClientOperationException : BanksDomainException
{
    private InvalidClientOperationException(string? message)
        : base(message) { }

    public static InvalidClientOperationException OnCreateWithoutNameOrSurname()
        => new InvalidClientOperationException("Unable to create client without name and surname");
}