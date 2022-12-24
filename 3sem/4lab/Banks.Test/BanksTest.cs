using Banks.BankAccounts;
using Banks.BankAccounts.Factories;
using Banks.Clocks;
using Banks.DepositInterestRateSelectors;
using Banks.Entities;
using Banks.Models;
using Banks.Models.BankConfigurations;
using Banks.Services;
using Banks.Transactions;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    private readonly CentralBank _centralBank;
    private readonly FrozenTimeClock _clock;

    public BanksTest()
    {
        _clock = new FrozenTimeClock();
        _centralBank = new CentralBank(_clock);
    }

    [Fact]
    public void TimePassed_InterestAccrues()
    {
        var creditConfiguration = new CreditBankAccountConfiguration(10, 100, 1);
        var depositConfiguration = new DepositBankAccountConfiguration(10, new TimeSpan(1000), DepositInterestRateSelector.GetBuilder().AddMoneyGap(new MoneyGap(1, 2, 0)).Build());
        var debitConfiguration = new DebitBankAccountConfiguration(10, 0.05m);
        var configuration = new BankConfiguration(creditConfiguration, debitConfiguration, depositConfiguration);

        var client = new Client("Ivan", "Ivanov");
        Bank bank = _centralBank.CreateBank("Test Bank", configuration);
        bank.AddClient(client);
        IBankAccount bankAccount = bank.CreateAccount(new DebitBankAccountFactory(), client, _clock);

        const decimal startDeposit = 100;
        bankAccount.Deposit(startDeposit);

        _clock.AddTimeSpan(TimeSpan.FromDays(31));

        Assert.NotEqual(startDeposit, bankAccount.Balance);
    }

    [Theory]
    [InlineData(3, 2, 1)]
    [InlineData(10, 10, 10)]
    [InlineData(100, 200, 50)]
    [InlineData(200, 250, 200)]
    public void CancelTransaction_MoneyBack(decimal moneyBefore1, decimal moneyBefore2, decimal transactionSum)
    {
        var creditConfiguration = new CreditBankAccountConfiguration(1000, 100, 1);
        var depositConfiguration = new DepositBankAccountConfiguration(1000, new TimeSpan(1000), DepositInterestRateSelector.GetBuilder().AddMoneyGap(new MoneyGap(1, 2, 0)).Build());
        var debitConfiguration = new DebitBankAccountConfiguration(1000, 0.05m);
        var configuration = new BankConfiguration(creditConfiguration, debitConfiguration, depositConfiguration);

        var client = new Client("Ivan", "Ivanov");
        Bank bank = _centralBank.CreateBank("Test Bank", configuration);
        bank.AddClient(client);

        IBankAccount bankAccount1 = bank.CreateAccount(new DebitBankAccountFactory(), client, _clock);
        IBankAccount bankAccount2 = bank.CreateAccount(new DebitBankAccountFactory(), client, _clock);
        bankAccount1.Deposit(moneyBefore1);
        bankAccount2.Deposit(moneyBefore2);

        ITransaction transaction = _centralBank.MakeTransferTransaction(transactionSum, bankAccount1, bankAccount2);
        transaction.Cancel();

        Assert.Equal(moneyBefore1, bankAccount1.Balance);
        Assert.Equal(moneyBefore2, bankAccount2.Balance);
    }
}