using Shops.Entities;
using Shops.ValueObjects;

namespace Shops.Models;

public class ProductSupply
{
    public ProductSupply(Product product, MoneyAmount price, ProductQuantity quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public Product Product { get; }
    public MoneyAmount Price { get; }
    public ProductQuantity Quantity { get; }
}
