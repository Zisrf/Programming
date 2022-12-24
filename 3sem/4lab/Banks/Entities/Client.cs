using Banks.Models;

namespace Banks.Entities;

public class Client
{
    public Client(string name, string surname, Address? address = null, Passport? passport = null)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(surname);

        Name = name;
        Surname = surname;
        Address = address;
        Passport = passport;

        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Surname { get; }
    public Address? Address { get; private set; }
    public Passport? Passport { get; private set; }

    public void SetAddress(Address address)
    {
        Address = address;
    }

    public void SetPassport(Passport passport)
    {
        Passport = passport;
    }

    public bool IsVerified()
    {
        return Address is not null && Passport is not null;
    }
}