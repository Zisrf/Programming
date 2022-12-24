using Shops.Entities;
using Shops.ValueObjects;

namespace Shops.Exceptions;

public class InvalidShopOperationException : ShopsDomainException
{
    private InvalidShopOperationException(string? message)
        : base(message)
    { }

    public static InvalidShopOperationException OnGetNonExistentItem(Shop shop, Product product)
        => new InvalidShopOperationException($"Shop {shop.Id} hasn't product {product.Id}");

    public static InvalidShopOperationException OnShortageOfProducts(Shop shop, ProductQuantity requestProductQuantity, ProductQuantity shopProductsQuantity)
        => new InvalidShopOperationException($"Shop {shop.Id} unable to sell {requestProductQuantity} products, shop has only {shopProductsQuantity}");

    public static InvalidShopOperationException OnAddAnotherProductToItem(Product product, Product otherProduct)
        => new InvalidShopOperationException($"Unable to add product {otherProduct.Id} to product {product.Id}");
}
