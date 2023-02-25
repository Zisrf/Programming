package BanksRemake.Domain.Transactions;

import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidTransactionOperationException;
import BanksRemake.Domain.Models.TransactionStatus;

import java.util.UUID;

/**
 * Transaction with one or more bank accounts.
 */
public interface Transaction {
    UUID getId();
    double getSum();
    TransactionStatus getStatus();

    void execute() throws InvalidTransactionOperationException, InvalidBankAccountOperationException;
    void cancel() throws InvalidTransactionOperationException, InvalidBankAccountOperationException;
}
