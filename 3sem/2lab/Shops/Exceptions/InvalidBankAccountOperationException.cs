using Shops.ValueObjects;

namespace Shops.Exceptions;

public class InvalidBankAccountOperationException : ShopsDomainException
{
    private InvalidBankAccountOperationException(string? message)
        : base(message)
    { }

    public static InvalidBankAccountOperationException OnIncorrectPayment(MoneyAmount balance, MoneyAmount payment)
        => new InvalidBankAccountOperationException($"Unable to make a payment, balance {balance} is smaller than payment {payment}");
}
