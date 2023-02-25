package BanksRemake.Domain.BankAccounts.Factories;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.CreditBankAccount;
import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;

/**
 * Factory creating credit bank accounts.
 */
public class CreditBankAccountFactory implements BankAccountFactory {
    @Override
    public BankAccount create(Bank bank, Client client, Clock clock) {
        return new CreditBankAccount(bank, client, clock);
    }
}
