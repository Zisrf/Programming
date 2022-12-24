namespace Shops.Exceptions;

public class InvalidShopManagerOperationException : ShopsDomainException
{
    private InvalidShopManagerOperationException(string? message)
        : base(message)
    { }

    public static InvalidShopManagerOperationException OnGetNonExistentProduct(Guid productId)
        => new InvalidShopManagerOperationException($"No product with id {productId}");

    public static InvalidShopManagerOperationException OnGetNonExistentShop(Guid shopId)
        => new InvalidShopManagerOperationException($"No shop with id {shopId}");
}
