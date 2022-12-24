namespace Banks.Exceptions;

public abstract class BanksDomainException : Exception
{
    protected BanksDomainException(string? message)
        : base(message) { }
}