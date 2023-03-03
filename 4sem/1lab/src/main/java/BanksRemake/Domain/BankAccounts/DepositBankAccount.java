package banksRemake.domain.bankAccounts;

import banksRemake.domain.clocks.Clock;
import banksRemake.domain.clocks.ClockSubscriber;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.models.bankConfigurations.DepositBankAccountConfiguration;

import java.util.Date;
import java.util.UUID;

/**
 * A bank account, without the possibility of withdrawing for a certain period of time and with an interest rate that depends on the amount.
 */
public class DepositBankAccount implements BankAccount, ClockSubscriber {
    private final UUID id;
    private final Client client;
    private final DepositBankAccountConfiguration configuration;
    private final Clock clock;
    private final double interestRate;
    private double balance;
    private double dailyPercents;
    private final Date endDate;

    public DepositBankAccount(double deposit, Bank bank, Client client, Clock clock) throws InvalidBankAccountOperationException {
        if (deposit < 0) {
            throw InvalidBankAccountOperationException.onInvalidMoneyAmount();
        }

        this.dailyPercents = 0;

        this.balance = deposit;
        this.client = client;
        this.configuration = bank.getConfiguration().depositBankAccountConfiguration();
        this.interestRate = configuration.selector().getInterestRate(deposit);
        this.endDate = Date.from(clock.getNowDate().toInstant().plus(configuration.duration()));
        this.clock = clock;

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

        return clock.getNowDate().after(endDate) && moneyAmount <= balance;
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
        if (clock.getNowDate().after(endDate)) {
            dailyPercents += balance * interestRate / 365;
        }
        else {
            clock.removeSubscriber(this);
        }
    }

    @Override
    public void reactOnMonthChanged() {
        if (clock.getNowDate().after(endDate)) {
            clock.removeSubscriber(this);
        }

        balance += dailyPercents;
        dailyPercents = 0;
    }
}
