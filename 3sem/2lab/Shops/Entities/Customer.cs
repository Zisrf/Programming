using Shops.ValueObjects;

namespace Shops.Entities;

public class Customer : IEquatable<Customer>
{
    public Customer(string name)
        : this(name, MoneyAmount.None)
    { }

    public Customer(string name, MoneyAmount balance)
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name;

        Id = Guid.NewGuid();
        BankAccount = new BankAccount(balance);
    }

    public Guid Id { get; }
    public string Name { get; }
    public BankAccount BankAccount { get; }

    public override bool Equals(object? obj)
        => Equals(obj as Customer);

    public bool Equals(Customer? other)
        => other?.Id.Equals(Id) ?? false;

    public override int GetHashCode()
        => Id.GetHashCode();
}
