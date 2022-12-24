using Banks.BankAccounts;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Transactions;

public class WithdrawTransaction : ITransaction
{
    public WithdrawTransaction(decimal sum, IBankAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);

        Id = Guid.NewGuid();
        Status = TransactionStatus.Created;

        Sum = sum;
        Account = account;
    }

    public Guid Id { get; }
    public decimal Sum { get; }
    public IBankAccount Account { get; }
    public TransactionStatus Status { get; private set; }

    public void Execute()
    {
        if (Status is not TransactionStatus.Created)
            throw InvalidTransactionOperationException.OnIncorrectExecuting();

        if (!Account.CanWithdraw(Sum))
            throw InvalidTransactionOperationException.OnInvalidTransaction();

        Account.Withdraw(Sum);

        Status = TransactionStatus.Executed;
    }

    public void Cancel()
    {
        if (Status is not TransactionStatus.Executed)
            throw InvalidTransactionOperationException.OnIncorrectCanceling();

        if (!Account.CanDeposit(Sum))
            throw InvalidTransactionOperationException.OnInvalidTransaction();

        Account.Deposit(Sum);

        Status = TransactionStatus.Canceled;
    }
}