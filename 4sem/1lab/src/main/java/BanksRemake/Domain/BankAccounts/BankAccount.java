package BanksRemake.Domain.BankAccounts;

import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;

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
