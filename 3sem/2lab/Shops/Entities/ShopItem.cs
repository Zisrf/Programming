using Shops.Exceptions;
using Shops.Models;
using Shops.ValueObjects;

namespace Shops.Entities;

public class ShopItem
{
    public ShopItem(ProductSupply supply)
    {
        ArgumentNullException.ThrowIfNull(supply);

        Product = supply.Product;
        Price = supply.Price;
        Quantity = supply.Quantity;
    }

    public Product Product { get; }
    public MoneyAmount Price { get; private set; }
    public ProductQuantity Quantity { get; private set; }

    public void AddSupply(ProductSupply supply)
    {
        ArgumentNullException.ThrowIfNull(supply);

        if (!Product.Equals(supply.Product))
            throw InvalidShopOperationException.OnAddAnotherProductToItem(Product, supply.Product);

        Price = supply.Price;
        Quantity += supply.Quantity;
    }

    public void DecreaseQuantity(ProductQuantity quantity)
    {
        Quantity -= quantity;
    }
}
