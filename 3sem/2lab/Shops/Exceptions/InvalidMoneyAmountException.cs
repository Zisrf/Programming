namespace Shops.Exceptions;

public class InvalidMoneyAmountException : ShopsDomainException
{
    public InvalidMoneyAmountException(decimal moneyAmount)
        : base($"{moneyAmount} is invalid negative money amount")
    { }
}
