using Shops.Exceptions;

namespace Shops.ValueObjects;

public readonly record struct ProductQuantity : IComparable<ProductQuantity>
{
    public ProductQuantity(int value)
    {
        if (value < 0)
            throw new InvalidProductQuantityException(value);

        Value = value;
    }

    public int Value { get; }

    public static ProductQuantity None => new ProductQuantity(0);

    public static ProductQuantity operator -(ProductQuantity a, ProductQuantity b)
        => new ProductQuantity(a.Value - b.Value);

    public static ProductQuantity operator +(ProductQuantity a, ProductQuantity b)
        => new ProductQuantity(a.Value + b.Value);

    public static bool operator <(ProductQuantity a, ProductQuantity b)
        => a.Value < b.Value;

    public static bool operator >(ProductQuantity a, ProductQuantity b)
        => a.Value > b.Value;

    public static bool operator <=(ProductQuantity a, ProductQuantity b)
        => a.Value <= b.Value;

    public static bool operator >=(ProductQuantity a, ProductQuantity b)
        => a.Value >= b.Value;

    public int CompareTo(ProductQuantity other)
        => Value.CompareTo(other.Value);

    public override string ToString()
        => Value.ToString();
}
