using Shops.Exceptions;
using Shops.Models;
using Shops.ValueObjects;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private readonly List<ShopItem> _items;

    public Shop(string name, string address)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(address);

        Name = name;
        Address = address;

        Id = Guid.NewGuid();
        BankAccount = new BankAccount();
        _items = new List<ShopItem>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }
    public BankAccount BankAccount { get; }

    public IReadOnlyList<ShopItem> Items => _items.AsReadOnly();

    public void AddSupplies(IEnumerable<ProductSupply> supplies)
    {
        foreach (ProductSupply supply in supplies)
            AddSupply(supply);
    }

    public void AddSupply(ProductSupply supply)
    {
        ArgumentNullException.ThrowIfNull(supply);

        ShopItem? item = FindItem(supply.Product);

        if (item is not null)
            item.AddSupply(supply);
        else
            _items.Add(new ShopItem(supply));
    }

    public ShopItem GetItem(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        return FindItem(product) ?? throw InvalidShopOperationException.OnGetNonExistentItem(this, product);
    }

    public ShopItem? FindItem(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        return _items.Find(i => i.Product.Equals(product));
    }

    public void Sell(Customer customer, IEnumerable<CustomerRequest> requests)
    {
        ArgumentNullException.ThrowIfNull(requests);

        customer.BankAccount.SendPaymentTo(BankAccount, GetTotalCost(requests));

        foreach (CustomerRequest request in requests)
            GetItem(request.Product).DecreaseQuantity(request.Quantity);
    }

    public void Sell(Customer customer, CustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        customer.BankAccount.SendPaymentTo(BankAccount, GetTotalCost(request));

        GetItem(request.Product).DecreaseQuantity(request.Quantity);
    }

    public MoneyAmount GetTotalCost(IEnumerable<CustomerRequest> requests)
    {
        ArgumentNullException.ThrowIfNull(requests);

        MoneyAmount totalCost = MoneyAmount.None;

        foreach (CustomerRequest request in requests)
            totalCost += GetTotalCost(request);

        return totalCost;
    }

    public MoneyAmount GetTotalCost(CustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!HaveEnoughProducts(request))
            throw InvalidShopOperationException.OnShortageOfProducts(this, request.Quantity, GetItem(request.Product).Quantity);

        return GetItem(request.Product).Price * request.Quantity;
    }

    public bool HaveEnoughProducts(IEnumerable<CustomerRequest> requests)
    {
        ArgumentNullException.ThrowIfNull(requests);

        return requests.All(r => HaveEnoughProducts(r));
    }

    public bool HaveEnoughProducts(CustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return GetItem(request.Product).Quantity >= request.Quantity;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Shop);

    public bool Equals(Shop? other)
        => other?.Id.Equals(Id) ?? false;

    public override int GetHashCode()
        => Id.GetHashCode();
}
