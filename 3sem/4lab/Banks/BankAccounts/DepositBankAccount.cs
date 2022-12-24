using Banks.Clocks;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models.BankConfigurations;

namespace Banks.BankAccounts;

public class DepositBankAccount : IBankAccount
{
    private readonly IClock _clock;
    private decimal _dailyPercents;

    public DepositBankAccount(decimal deposit, Bank bank, Client client, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(clock);

        if (deposit < 0)
            throw InvalidBankAccountOperationException.OnInvalidMoneyAmount();

        _dailyPercents = 0;

        Balance = deposit;
        Client = client;
        Configuration = bank.Configuration.DepositConfiguration;
        InterestRate = bank.Configuration.DepositConfiguration.Selector.GetInterestRate(deposit);
        EndDate = clock.Now + bank.Configuration.DepositConfiguration.Duration;
        _clock = clock;

        Id = Guid.NewGuid();

        clock.DayChanged += OnDayChanged;
        clock.MonthChanged += OnMonthChanged;
    }

    public Guid Id { get; }
    public Client Client { get; }
    public DepositBankAccountConfiguration Configuration { get; }
    public decimal InterestRate { get; }
    public decimal Balance { get; private set; }
    public DateTime EndDate { get; }

    public bool CanDeposit(decimal moneyAmount)
    {
        return Client.IsVerified() || moneyAmount <= Configuration.UnverifiedLimit;
    }

    public bool CanWithdraw(decimal moneyAmount)
    {
        if (!Client.IsVerified() && moneyAmount > Configuration.UnverifiedLimit)
            return false;

        return _clock.Now >= EndDate && moneyAmount <= Balance;
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
        if (_clock.Now < EndDate)
            _dailyPercents += Balance * InterestRate / 365;
        else
            _clock.DayChanged -= OnDayChanged;
    }

    private void OnMonthChanged()
    {
        if (_clock.Now >= EndDate)
            _clock.MonthChanged -= OnMonthChanged;

        Balance += _dailyPercents;
        _dailyPercents = 0;
    }
}