package BanksRemake.Domain.BankAccounts;

import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Clocks.ClockSubscriber;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Models.BankConfigurations.DebitBankAccountConfiguration;

import java.util.UUID;

/**
 * Bank account that earns interest every month.
 */
public class DebitBankAccount implements BankAccount, ClockSubscriber {
    private final UUID id;
    private final Client client;
    private final DebitBankAccountConfiguration configuration;
    private double balance;
    private double dailyPercents;

    public DebitBankAccount(Bank bank, Client client, Clock clock)  {
        this.balance = 0;
        this.dailyPercents = 0;

        this.client = client;
        this.configuration = bank.getConfiguration().debitBankAccountConfiguration();

        id = UUID.randomUUID();

        clock.addSubscriber(this);
    }

    @Override
    public UUID getId() {
        return id;
    }

    @Override
    public double getBalance() {
        return balance;
    }

    @Override
    public Client getClient() {
        return client;
    }

    @Override
    public boolean canDeposit(double moneyAmount) throws InvalidBankAccountOperationException {
        if (moneyAmount < 0) {
            throw InvalidBankAccountOperationException.onInvalidMoneyAmount();
        }

        return client.isVerified() || moneyAmount <= configuration.unverifiedLimit();
    }

    @Override
    public boolean canWithdraw(double moneyAmount) throws InvalidBankAccountOperationException {
        if (moneyAmount < 0) {
            throw InvalidBankAccountOperationException.onInvalidMoneyAmount();
        }

        if (!client.isVerified() && moneyAmount > configuration.unverifiedLimit()) {
            return false;
        }

        return moneyAmount <= balance;
    }

    @Override
    public void deposit(double moneyAmount) throws InvalidBankAccountOperationException {
        if (moneyAmount < 0) {
            throw InvalidBankAccountOperationException.onInvalidMoneyAmount();
        }

        balance += moneyAmount;
    }

    @Override
    public void withdraw(double moneyAmount) throws InvalidBankAccountOperationException {
        if (moneyAmount < 0) {
            throw InvalidBankAccountOperationException.onInvalidMoneyAmount();
        }

        if (!canWithdraw(moneyAmount)) {
            throw InvalidBankAccountOperationException.onShortageOfMoney(moneyAmount, balance);
        }

        balance -= moneyAmount;
    }

    @Override
    public void reactOnDayChanged() {
        dailyPercents += balance * configuration.interestRate() / 365;
    }

    @Override
    public void reactOnMonthChanged() {
        balance += dailyPercents;
        dailyPercents = 0;
    }
}
