using Reports.Domain.Common.Exceptions.ValueObjects;

namespace Reports.Core.ValueObjects;

public readonly record struct AccessLevel
{
    public AccessLevel(int value)
    {
        if (value < 0)
            throw InvalidAccessLevelException.OnNegativeValue();

        Value = value;
    }

    public int Value { get; }

    public bool CanAccess(AccessLevel other)
    {
        return Value >= other.Value;
    }

    public static implicit operator AccessLevel(int value)
        => new AccessLevel(value);

    public static explicit operator int(AccessLevel accessLevel)
        => accessLevel.Value;
}