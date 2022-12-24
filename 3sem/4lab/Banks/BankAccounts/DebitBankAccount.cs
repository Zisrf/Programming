using Banks.Clocks;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models.BankConfigurations;

namespace Banks.BankAccounts;

public class DebitBankAccount : IBankAccount
{
    private decimal _dailyPercents;

    public DebitBankAccount(Bank bank, Client client, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(clock);

        Balance = 0;
        _dailyPercents = 0;

        Client = client;
        Configuration = bank.Configuration.DebitConfiguration;

        Id = Guid.NewGuid();

        clock.DayChanged += OnDayChanged;
        clock.MonthChanged += OnMonthChanged;
    }

    public Guid Id { get; }
    public Client Client { get; }
    public DebitBankAccountConfiguration Configuration { get; }
    public decimal Balance { get; private set; }

    public bool CanDeposit(decimal moneyAmount)
    {
        return Client.IsVerified() || moneyAmount <= Configuration.UnverifiedLimit;
    }

    public bool CanWithdraw(decimal moneyAmount)
    {
        if (!Client.IsVerified() && moneyAmount > Configuration.UnverifiedLimit)
            return false;

        return moneyAmount <= Balance;
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
            throw InvalidBankAccountOperationException.OnShortageOfMoney(moneyAmount, Balance);

        Balance -= moneyAmount;
    }

    private void OnDayChanged()
    {
        _dailyPercents += Balance * Configuration.InterestRate / 365;
    }

    private void OnMonthChanged()
    {
        Balance += _dailyPercents;
        _dailyPercents = 0;
    }
}