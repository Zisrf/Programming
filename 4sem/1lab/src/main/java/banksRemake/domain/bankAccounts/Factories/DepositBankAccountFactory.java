package banksRemake.domain.bankAccounts.Factories;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.bankAccounts.DepositBankAccount;
import banksRemake.domain.clocks.Clock;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import lombok.Getter;

/**
 * Factory creating deposit bank accounts.
 */
public class DepositBankAccountFactory implements BankAccountFactory {
    @Getter
    private final double deposit;

    public DepositBankAccountFactory(double deposit) {
        this.deposit = deposit;
    }

    @Override
    public BankAccount create(Bank bank, Client client, Clock clock) throws InvalidBankAccountOperationException {
        return new DepositBankAccount(deposit, bank, client, clock);
    }
}
