using Shops.Exceptions;

namespace Shops.ValueObjects;

public readonly record struct MoneyAmount : IComparable<MoneyAmount>
{
    public MoneyAmount(decimal value)
    {
        if (value < 0)
            throw new InvalidMoneyAmountException(value);

        Value = value;
    }

    public decimal Value { get; }

    public static MoneyAmount None => new MoneyAmount(0);

    public static MoneyAmount operator *(MoneyAmount price, ProductQuantity quantity)
        => new MoneyAmount(price.Value * quantity.Value);

    public static MoneyAmount operator -(MoneyAmount a, MoneyAmount b)
        => new MoneyAmount(a.Value - b.Value);

    public static MoneyAmount operator +(MoneyAmount a, MoneyAmount b)
        => new MoneyAmount(a.Value + b.Value);

    public static bool operator <(MoneyAmount a, MoneyAmount b)
        => a.Value < b.Value;

    public static bool operator >(MoneyAmount a, MoneyAmount b)
        => a.Value > b.Value;

    public static bool operator <=(MoneyAmount a, MoneyAmount b)
        => a.Value <= b.Value;

    public static bool operator >=(MoneyAmount a, MoneyAmount b)
        => a.Value >= b.Value;

    public int CompareTo(MoneyAmount other)
        => Value.CompareTo(other.Value);

    public override string ToString()
        => Value.ToString();
}