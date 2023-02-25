package BanksRemake.Domain.BankAccounts.Factories;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;

/**
 * An abstract factory that creates different types of bank accounts.
 */
public interface BankAccountFactory {
    BankAccount create(Bank bank, Client client, Clock clock) throws InvalidBankOperationException, InvalidBankAccountOperationException;
}
