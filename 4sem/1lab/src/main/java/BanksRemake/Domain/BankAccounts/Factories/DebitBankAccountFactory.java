package BanksRemake.Domain.BankAccounts.Factories;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.DebitBankAccount;
import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;

/**
 * Factory creating debit bank accounts.
 */
public class DebitBankAccountFactory implements BankAccountFactory {
    @Override
    public BankAccount create(Bank bank, Client client, Clock clock) {
        return new DebitBankAccount(bank, client, clock);
    }
}
