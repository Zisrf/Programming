using Banks.BankAccounts;
using Banks.Clocks;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Models.BankConfigurations;
using Banks.Transactions;

namespace Banks.Services;

public class CentralBank
{
    private readonly List<ITransaction> _transactions;
    private readonly List<Bank> _banks;

    public CentralBank(IClock clock)
    {
        ArgumentNullException.ThrowIfNull(clock);

        Clock = clock;

        _banks = new List<Bank>();
        _transactions = new List<ITransaction>();
    }

    public IClock Clock { get; }

    public IEnumerable<Bank> Banks => _banks;
    public IEnumerable<ITransaction> Transactions => _transactions;

    public Bank GetBankById(Guid id)
    {
        return _banks.Find(b => b.Id.Equals(id)) ?? throw InvalidCentralBankOperationException.OnGetNonExistentBank(id);
    }

    public Bank CreateBank(string name, BankConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configuration);

        var bank = new Bank(name, configuration, this);
        _banks.Add(bank);
        return bank;
    }

    public ITransaction GetTransactionById(Guid id)
    {
        return _transactions.Find(t => t.Id.Equals(id)) ?? throw InvalidCentralBankOperationException.OnGetNonExistentTransaction(id);
    }

    public ITransaction MakeDepositTransaction(decimal sum, IBankAccount account)
    {
        var transaction = new DepositTransaction(sum, account);

        transaction.Execute();
        _transactions.Add(transaction);

        return transaction;
    }

    public ITransaction MakeWithdrawTransaction(decimal sum, IBankAccount account)
    {
        var transaction = new WithdrawTransaction(sum, account);

        transaction.Execute();
        _transactions.Add(transaction);

        return transaction;
    }

    public ITransaction MakeTransferTransaction(decimal sum, IBankAccount sender, IBankAccount receiver)
    {
        var transaction = new TransferTransaction(sum, sender, receiver);

        transaction.Execute();
        _transactions.Add(transaction);

        return transaction;
    }
}