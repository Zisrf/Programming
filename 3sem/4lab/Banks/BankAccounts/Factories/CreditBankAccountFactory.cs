using Banks.Clocks;
using Banks.Entities;

namespace Banks.BankAccounts.Factories;

public class CreditBankAccountFactory : IBankAccountFactory
{
    public IBankAccount Create(Bank bank, Client client, IClock clock)
    {
        return new CreditBankAccount(bank, client, clock);
    }
}