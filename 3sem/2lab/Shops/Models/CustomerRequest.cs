using Shops.Entities;
using Shops.ValueObjects;

namespace Shops.Models;

public class CustomerRequest
{
    public CustomerRequest(Product product, ProductQuantity quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public ProductQuantity Quantity { get; }
}
