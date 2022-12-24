using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopManager
{
    private readonly List<Product> _products;
    private readonly List<Shop> _shops;

    public ShopManager()
    {
        _products = new List<Product>();
        _shops = new List<Shop>();
    }

    public IReadOnlyList<Product> Products => _products.AsReadOnly();
    public IReadOnlyList<Shop> Shops => _shops.AsReadOnly();

    public Product CreateProduct(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        var newProduct = new Product(name);
        _products.Add(newProduct);
        return newProduct;
    }

    public Shop CreateShop(string name, string address)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(address);

        var newShop = new Shop(name, address);
        _shops.Add(newShop);
        return newShop;
    }

    public Product GetProductById(Guid productId)
    {
        return FindProductById(productId) ?? throw InvalidShopManagerOperationException.OnGetNonExistentProduct(productId);
    }

    public Shop GetShopById(Guid shopId)
    {
        return FindShopById(shopId) ?? throw InvalidShopManagerOperationException.OnGetNonExistentShop(shopId);
    }

    public Product? FindProductById(Guid productId)
    {
        return _products.Find(p => p.Id.Equals(productId));
    }

    public Shop? FindShopById(Guid shopId)
    {
        return _shops.Find(s => s.Id.Equals(shopId));
    }

    public Shop? FindCheapestShop(IEnumerable<CustomerRequest> requests)
    {
        ArgumentNullException.ThrowIfNull(requests);

        return _shops
            .Where(s => s.HaveEnoughProducts(requests))
            .MinBy(s => s.GetTotalCost(requests));
    }
}
