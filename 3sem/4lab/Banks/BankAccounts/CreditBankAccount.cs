using Banks.Clocks;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models.BankConfigurations;

namespace Banks.BankAccounts;

public class CreditBankAccount : IBankAccount
{
    public CreditBankAccount(Bank bank, Client client, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(clock);

        Balance = 0;
        Id = Guid.NewGuid();

        Client = client;
        Configuration = bank.Configuration.CreditConfiguration;

        clock.DayChanged += OnDayChanged;
    }

    public Guid Id { get; }
    public Client Client { get; }
    public CreditBankAccountConfiguration Configuration { get; }
    public decimal Balance { get; private set; }

    public bool CanDeposit(decimal moneyAmount)
    {
        return Client.IsVerified() || moneyAmount <= Configuration.UnverifiedLimit;
    }

    public bool CanWithdraw(decimal moneyAmount)
    {
        if (!Client.IsVerified() && moneyAmount > Configuration.UnverifiedLimit)
            return false;

        return moneyAmount <= Balance + Configuration.CreditLimit;
    }

    public void Deposit(decimal moneyAmount)
    {
        if (moneyAmount < 0)
            throw InvalidBankAccountOperationException.OnInvalidMoneyAmount();

        Balance += moneyAmount;
    }

    public void Withdraw(decimal moneyAmount)
    {
        if (moneyAmount < 0)
            throw InvalidBankAccountOperationException.OnInvalidMoneyAmount();

        if (!CanWithdraw(moneyAmount))
            InvalidBankAccountOperationException.OnShortageOfMoney(moneyAmount, Balance);

        Balance -= moneyAmount;
    }

    private void OnDayChanged()
    {
        if (Balance < 0 && Balance > -Configuration.CreditLimit)
            Balance -= Configuration.Commission;
    }
}