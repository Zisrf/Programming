using Banks.Clocks;
using Banks.Entities;
using Banks.Exceptions;

namespace Banks.BankAccounts.Factories;

public class DepositBankAccountFactory : IBankAccountFactory
{
    public DepositBankAccountFactory(decimal deposit)
    {
        if (deposit < 0)
            throw InvalidBankAccountOperationException.OnInvalidMoneyAmount();

        Deposit = deposit;
    }

    public decimal Deposit { get; }

    public IBankAccount Create(Bank bank, Client client, IClock clock)
    {
        return new DepositBankAccount(Deposit, bank, client, clock);
    }
}