package banksRemake.domain.bankAccounts;

import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;

import java.util.UUID;

/**
 * Bank account where the client's money is stored.
 */
public interface BankAccount {
    UUID getId();
    double getBalance();
    Client getClient();

    boolean canDeposit(double moneyAmount) throws InvalidBankAccountOperationException;
    boolean canWithdraw(double moneyAmount) throws InvalidBankAccountOperationException;

    void deposit(double moneyAmount) throws InvalidBankAccountOperationException;
    void withdraw(double moneyAmount) throws InvalidBankAccountOperationException;
}
