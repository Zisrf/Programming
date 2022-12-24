namespace Shops.Exceptions;

public class InvalidProductQuantityException : ShopsDomainException
{
    public InvalidProductQuantityException(int productQuantity)
        : base($"{productQuantity} is invalid negative product quntity")
    { }
}
