using Banks.Clocks;
using Banks.Entities;

namespace Banks.BankAccounts.Factories;

public interface IBankAccountFactory
{
    IBankAccount Create(Bank bank, Client client, IClock clock);
}