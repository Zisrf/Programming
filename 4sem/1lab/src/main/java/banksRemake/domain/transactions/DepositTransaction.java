package banksRemake.domain.transactions;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidTransactionOperationException;
import banksRemake.domain.models.TransactionStatus;

import java.util.UUID;

/**
 * The operation of crediting money to the account.
 */
public class DepositTransaction implements Transaction {
    private final UUID id;
    private final double sum;
    private final BankAccount account;
    private TransactionStatus status;

    public DepositTransaction(double sum, BankAccount account) {
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
        if (status != TransactionStatus.Created) {
            throw InvalidTransactionOperationException.onIncorrectExecuting();
        }

        if (!account.canDeposit(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        account.deposit(sum);

        status = TransactionStatus.Executed;
    }

    @Override
    public void cancel() throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        if (status != TransactionStatus.Executed){
            throw InvalidTransactionOperationException.onIncorrectCanceling();
        }

        if (!account.canWithdraw(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        account.withdraw(sum);

        status = TransactionStatus.Canceled;
    }
}
