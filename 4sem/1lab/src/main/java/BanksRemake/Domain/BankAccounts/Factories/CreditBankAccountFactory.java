package banksRemake.domain.bankAccounts.Factories;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.bankAccounts.CreditBankAccount;
import banksRemake.domain.clocks.Clock;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;

/**
 * Factory creating credit bank accounts.
 */
public class CreditBankAccountFactory implements BankAccountFactory {
    @Override
    public BankAccount create(Bank bank, Client client, Clock clock) {
        return new CreditBankAccount(bank, client, clock);
    }
}
