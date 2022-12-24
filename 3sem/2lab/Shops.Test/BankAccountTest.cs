using Shops.Entities;
using Shops.Exceptions;
using Shops.ValueObjects;

using Xunit;

namespace Shops.Test;

public class BankAccountTest
{
    [Fact]
    public void MakeIncorrectPayment_ThrowException()
    {
        var payment = new MoneyAmount(1);
        var bankAccount1 = new BankAccount(MoneyAmount.None);
        var bankAccount2 = new BankAccount(MoneyAmount.None);

        Assert.Throws<InvalidBankAccountOperationException>(()
            => bankAccount1.SendPaymentTo(bankAccount2, payment));
    }

    [Fact]
    public void MakeCorrectPayment_BalancesChangedCorrectly()
    {
        var balance1 = new MoneyAmount(3);
        var balance2 = new MoneyAmount(2);
        var payment = new MoneyAmount(1);
        var bankAccount1 = new BankAccount(balance1);
        var bankAccount2 = new BankAccount(balance2);

        bankAccount1.SendPaymentTo(bankAccount2, payment);

        Assert.Equal(balance1 - payment, bankAccount1.Balance);
        Assert.Equal(balance2 + payment, bankAccount2.Balance);
    }
}
