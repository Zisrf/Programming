package banksRemake.domain.bankAccounts.Factories;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.bankAccounts.DebitBankAccount;
import banksRemake.domain.clocks.Clock;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;

/**
 * Factory creating debit bank accounts.
 */
public class DebitBankAccountFactory implements BankAccountFactory {
    @Override
    public BankAccount create(Bank bank, Client client, Clock clock) {
        return new DebitBankAccount(bank, client, clock);
    }
}
