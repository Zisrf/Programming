namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    public Product(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        Name = name;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; }

    public override bool Equals(object? obj)
        => Equals(obj as Product);

    public bool Equals(Product? other)
        => other?.Id.Equals(Id) ?? false;

    public override int GetHashCode()
        => Id.GetHashCode();
}
