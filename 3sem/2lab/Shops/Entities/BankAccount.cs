using Shops.Exceptions;
using Shops.ValueObjects;

namespace Shops.Entities;

public class BankAccount : IEquatable<BankAccount>
{
    public BankAccount()
        : this(MoneyAmount.None)
    { }

    public BankAccount(MoneyAmount balance)
    {
        Balance = balance;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public MoneyAmount Balance { get; private set; }

    public void AcceptPayment(MoneyAmount payment)
    {
        Balance += payment;
    }

    public void SendPaymentTo(BankAccount receiver, MoneyAmount payment)
    {
        ArgumentNullException.ThrowIfNull(receiver);

        if (Balance < payment)
            throw InvalidBankAccountOperationException.OnIncorrectPayment(Balance, payment);

        receiver.AcceptPayment(payment);
        Balance -= payment;
    }

    public override bool Equals(object? obj)
        => Equals(obj as BankAccount);

    public bool Equals(BankAccount? other)
        => other?.Id.Equals(Id) ?? false;

    public override int GetHashCode()
        => Id.GetHashCode();
}
