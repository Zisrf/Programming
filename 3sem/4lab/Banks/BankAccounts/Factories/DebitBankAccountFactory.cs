using Banks.Clocks;
using Banks.Entities;

namespace Banks.BankAccounts.Factories;

public class DebitBankAccountFactory : IBankAccountFactory
{
    public IBankAccount Create(Bank bank, Client client, IClock clock)
    {
        return new DebitBankAccount(bank, client, clock);
    }
}