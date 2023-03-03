package banksRemake.domain.transactions;

import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidTransactionOperationException;
import banksRemake.domain.models.TransactionStatus;

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
