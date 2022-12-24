using Banks.BankAccounts;
using Banks.BankAccounts.Factories;
using Banks.Clocks;
using Banks.Exceptions;
using Banks.Models.BankConfigurations;
using Banks.Services;

namespace Banks.Entities;

public class Bank
{
    private readonly HashSet<Client> _clients;
    private readonly HashSet<IBankAccount> _accounts;

    public Bank(string name, BankConfiguration configuration, CentralBank centralBank)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(centralBank);

        Name = name;
        Configuration = configuration;
        CentralBank = centralBank;

        Id = Guid.NewGuid();
        _clients = new HashSet<Client>();
        _accounts = new HashSet<IBankAccount>();
    }

    public Guid Id { get; }
    public string Name { get; }
    public BankConfiguration Configuration { get; }
    public CentralBank CentralBank { get; }

    public IEnumerable<Client> Clients => _clients;
    public IEnumerable<IBankAccount> Accounts => _accounts;

    public void AddClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if (!_clients.Add(client))
            throw InvalidBankOperationException.OnAddExistingClient();
    }

    public IBankAccount CreateAccount(IBankAccountFactory factory, Client client, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(clock);

        if (!_clients.Contains(client))
            throw InvalidBankOperationException.OnAddAccountToUnregisteredClient();

        IBankAccount account = factory.Create(this, client, clock);
        _accounts.Add(account);

        return account;
    }
}