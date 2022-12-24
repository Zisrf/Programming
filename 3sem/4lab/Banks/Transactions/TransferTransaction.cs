using Banks.BankAccounts;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Transactions;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(decimal sum, IBankAccount sender, IBankAccount receiver)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(receiver);

        if (sender.Id == receiver.Id)
            throw InvalidTransactionOperationException.OnTransactionToSameAccount();

        Id = Guid.NewGuid();
        Status = TransactionStatus.Created;

        Sum = sum;
        Sender = sender;
        Receiver = receiver;
    }

    public Guid Id { get; }
    public decimal Sum { get; }
    public IBankAccount Sender { get; }
    public IBankAccount Receiver { get; }
    public TransactionStatus Status { get; private set; }

    public void Execute()
    {
        if (Status is not TransactionStatus.Created)
            throw InvalidTransactionOperationException.OnIncorrectExecuting();

        if (!Sender.CanWithdraw(Sum) || !Receiver.CanDeposit(Sum))
            throw InvalidTransactionOperationException.OnInvalidTransaction();

        Sender.Withdraw(Sum);
        Receiver.Deposit(Sum);

        Status = TransactionStatus.Executed;
    }

    public void Cancel()
    {
        if (Status is not TransactionStatus.Executed)
            throw InvalidTransactionOperationException.OnIncorrectCanceling();

        if (!Receiver.CanWithdraw(Sum) || !Sender.CanDeposit(Sum))
            throw InvalidTransactionOperationException.OnInvalidTransaction();

        Receiver.Withdraw(Sum);
        Sender.Deposit(Sum);

        Status = TransactionStatus.Canceled;
    }
}