using Banks.Entities;

namespace Banks.BankAccounts;

public interface IBankAccount
{
    Guid Id { get; }
    decimal Balance { get; }
    Client Client { get; }

    bool CanDeposit(decimal moneyAmount);
    bool CanWithdraw(decimal moneyAmount);

    void Deposit(decimal moneyAmount);
    void Withdraw(decimal moneyAmount);
}