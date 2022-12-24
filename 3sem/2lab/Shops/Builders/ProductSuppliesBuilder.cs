using Shops.Models;

namespace Shops.Builders;

public class ProductSuppliesBuilder
{
    private List<ProductSupply> _supplies;

    public ProductSuppliesBuilder()
    {
        _supplies = new List<ProductSupply>();
    }

    public ProductSuppliesBuilder Reset()
    {
        _supplies = new List<ProductSupply>();

        return this;
    }

    public ProductSuppliesBuilder AddSupply(ProductSupply supply)
    {
        ArgumentNullException.ThrowIfNull(supply);

        _supplies.Add(supply);

        return this;
    }

    public IEnumerable<ProductSupply> GetResult()
    {
        IEnumerable<ProductSupply> result = _supplies;

        Reset();

        return result;
    }
}
