package BanksRemake.Domain.Transactions;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidTransactionOperationException;
import BanksRemake.Domain.Models.TransactionStatus;

import java.util.UUID;

/**
 * The operation of transferring money from one account to another.
 */
public class TransferTransaction implements Transaction {
    private final UUID id;
    private final double sum;
    private final BankAccount sender;
    private final BankAccount receiver;
    private TransactionStatus status;

    public TransferTransaction(double sum, BankAccount sender, BankAccount receiver) throws InvalidTransactionOperationException {
        if (sender.getId() == receiver.getId()) {
            throw InvalidTransactionOperationException.onTransactionToSameAccount();
        }

        this.status = TransactionStatus.Created;

        this.sum = sum;
        this.sender = sender;
        this.receiver = receiver;

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

        if (!sender.canWithdraw(sum) || !receiver.canDeposit(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        sender.withdraw(sum);
        receiver.deposit(sum);

        status = TransactionStatus.Executed;
    }

    @Override
    public void cancel() throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        if (status != TransactionStatus.Executed){
            throw InvalidTransactionOperationException.onIncorrectCanceling();
        }

        if (!receiver.canWithdraw(sum) || !sender.canDeposit(sum)) {
            throw InvalidTransactionOperationException.onInvalidTransaction();
        }

        receiver.withdraw(sum);
        sender.deposit(sum);

        status = TransactionStatus.Canceled;
    }
}
