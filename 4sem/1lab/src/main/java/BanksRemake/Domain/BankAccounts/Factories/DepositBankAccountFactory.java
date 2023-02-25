package BanksRemake.Domain.BankAccounts.Factories;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.DepositBankAccount;
import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
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
