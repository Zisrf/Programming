using Banks.Models;

namespace Banks.Transactions;

public interface ITransaction
{
    Guid Id { get; }
    decimal Sum { get; }
    TransactionStatus Status { get; }

    void Execute();
    void Cancel();
}