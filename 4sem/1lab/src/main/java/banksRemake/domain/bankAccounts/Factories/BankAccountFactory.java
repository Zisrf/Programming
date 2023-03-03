package banksRemake.domain.bankAccounts.Factories;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.clocks.Clock;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;

/**
 * An abstract factory that creates different types of bank accounts.
 */
public interface BankAccountFactory {
    BankAccount create(Bank bank, Client client, Clock clock) throws InvalidBankOperationException, InvalidBankAccountOperationException;
}
