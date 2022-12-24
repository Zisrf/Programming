namespace Shops.Exceptions;

public abstract class ShopsDomainException : Exception
{
    protected ShopsDomainException(string? message)
        : base(message)
    { }
}
