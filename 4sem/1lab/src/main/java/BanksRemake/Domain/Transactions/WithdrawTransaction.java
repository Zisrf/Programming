package BanksRemake.Domain.Transactions;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidTransactionOperationException;
import BanksRemake.Domain.Models.TransactionStatus;

import java.util.UUID;

/**
 * The operation of withdrawing money from the account.
 */
public class WithdrawTransaction implements Transaction {
    private final UUID id;
    private final double sum;
    private final BankAccount account;
    private TransactionStatus status;

    public WithdrawTransaction(double sum, BankAccount account) {
        this.status = TransactionStatus.Created;

        this.sum = sum;
        this.account = account;

        this.id = UUID.randomUUID();
    }

    @Override
    public UUID getId() {
        return id;
    }

    @Override
    public double getSum() {
        return sum;
    }

    @Override
    public TransactionStatus getStatus() {
        return status;
    }

    @Override
    public void execute() throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        if (status != TransactionStatus.Created){
            throw InvalidTransactionOperationException.onIncorrectExecuting();
        }

        if (!account.canWithdraw(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        account.withdraw(sum);

        status = TransactionStatus.Executed;
    }

    @Override
    public void cancel() throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        if (status != TransactionStatus.Executed){
            throw InvalidTransactionOperationException.onIncorrectCanceling();
        }

        if (!account.canDeposit(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        account.deposit(sum);

        status = TransactionStatus.Canceled;
    }
}
