package banksRemake.domain.bankAccounts;

import banksRemake.domain.clocks.Clock;
import banksRemake.domain.clocks.ClockSubscriber;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.models.bankConfigurations.CreditBankAccountConfiguration;

import java.util.UUID;

/**
 * A bank account that allows you to go negative by the amount of the credit limit.
 */
public class CreditBankAccount implements BankAccount, ClockSubscriber {
    private final UUID id;
    private final Client client;
    private final CreditBankAccountConfiguration configuration;
    private double balance;

    public CreditBankAccount(Bank bank, Client client, Clock clock) {
        this.balance = 0;

        this.client = client;
        this.configuration = bank.getConfiguration().creditBankAccountConfiguration();

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

        return moneyAmount <= balance + configuration.creditLimit();
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
        if (balance < 0 && balance > -configuration.creditLimit()) {
            balance -= configuration.commission();
        }
    }

    @Override
    public void reactOnMonthChanged() {
    }
}
