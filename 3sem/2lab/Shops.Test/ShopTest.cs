using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.ValueObjects;
using Xunit;

namespace Shops.Test;

public class ShopTest
{
    [Fact]
    public void SellProduct_BalanceAndQuantityChangedCorrectly()
    {
        var moneyBefore = new MoneyAmount(100);
        var productPrice = new MoneyAmount(50);
        var productQuantity = new ProductQuantity(2);
        var productToBuyQuantity = new ProductQuantity(1);
        var customer = new Customer("Customer", moneyBefore);
        var product = new Product("Product");
        var shop = new Shop("Shop", "address");

        shop.AddSupply(new ProductSupply(product, productPrice, productQuantity));
        shop.Sell(customer, new CustomerRequest(product, productToBuyQuantity));

        Assert.Equal(moneyBefore - (productPrice * productToBuyQuantity), customer.BankAccount.Balance);
        Assert.Equal(productPrice * productToBuyQuantity, shop.BankAccount.Balance);
        Assert.Equal(productQuantity - productToBuyQuantity, shop.GetItem(product).Quantity);
    }

    [Fact]
    public void SellMoreProductThatShopHas_ThrowException()
    {
        var productQuantity = new ProductQuantity(0);
        var productToBuyQuantity = new ProductQuantity(1);
        var customer = new Customer("Customer", MoneyAmount.None);
        var product = new Product("Product");
        var shop = new Shop("Shop", "address");

        shop.AddSupply(new ProductSupply(product, MoneyAmount.None, productQuantity));

        Assert.Throws<InvalidShopOperationException>(()
            => shop.Sell(customer, new CustomerRequest(product, productToBuyQuantity)));
    }

    [Fact]
    public void AddExistingProductToShop_PriceAndQuantityChangedCorrectly()
    {
        var price1 = new MoneyAmount(1);
        var price2 = new MoneyAmount(2);
        var quantity1 = new ProductQuantity(3);
        var quantity2 = new ProductQuantity(4);
        var shop = new Shop("Shop", "address");
        var product = new Product("Product");

        shop.AddSupply(new ProductSupply(product, price1, quantity1));
        shop.AddSupply(new ProductSupply(product, price2, quantity2));

        Assert.Equal(price2, shop.FindItem(product)?.Price);
        Assert.Equal(quantity1 + quantity2, shop.FindItem(product)?.Quantity);
    }
}
