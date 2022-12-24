namespace Banks.Models;

public readonly record struct Address
{
    public Address(string country, string city, string street, string house)
    {
        ArgumentNullException.ThrowIfNull(country);
        ArgumentNullException.ThrowIfNull(city);
        ArgumentNullException.ThrowIfNull(street);
        ArgumentNullException.ThrowIfNull(house);

        Country = country;
        City = city;
        Street = street;
        House = house;
    }

    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string House { get; }

    public override string ToString()
    {
        return $"{Country}, {City}, {Street}, {House}";
    }
}