package BanksRemake;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.Factories.DebitBankAccountFactory;
import BanksRemake.Domain.Clocks.FrozenTimeClock;
import BanksRemake.Domain.DepositInterestRateSelectors.DepositInterestRateSelectorImpl;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.*;
import BanksRemake.Domain.Models.BankConfigurations.BankConfiguration;
import BanksRemake.Domain.Models.BankConfigurations.CreditBankAccountConfiguration;
import BanksRemake.Domain.Models.BankConfigurations.DebitBankAccountConfiguration;
import BanksRemake.Domain.Models.BankConfigurations.DepositBankAccountConfiguration;
import BanksRemake.Domain.Models.MoneyGap;
import BanksRemake.Domain.Services.CentralBank;
import BanksRemake.Domain.Transactions.Transaction;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.time.Instant;
import java.time.Period;
import java.util.Date;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotEquals;

public class BanksTest {
    private CentralBank centralBank;
    private FrozenTimeClock clock;

    @BeforeEach
    public void setup()
    {
        clock = new FrozenTimeClock(Date.from(Instant.now()));
        centralBank = new CentralBank(clock);
    }

    @Test
    public void timePassed_interestAccrues() throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidMoneyGapException, InvalidDepositInterestRateSelectorException {
        var creditConfiguration = new CreditBankAccountConfiguration(10, 100, 1);
        var depositConfiguration = new DepositBankAccountConfiguration(10, Period.ofDays(1), DepositInterestRateSelectorImpl.getBuilder().addMoneyGap(new MoneyGap(1, 2, 0)).build());
        var debitConfiguration = new DebitBankAccountConfiguration(10, 0.05);
        var configuration = new BankConfiguration(creditConfiguration, debitConfiguration, depositConfiguration);

        var client = new Client("Ivan Ivanov");
        Bank bank = centralBank.createBank("Test Bank", configuration);
        bank.addClient(client);
        BankAccount bankAccount = bank.createAccount(new DebitBankAccountFactory(), client, clock);

        double startDeposit = 100;
        bankAccount.deposit(startDeposit);

        clock.addPeriod(Period.ofDays(31));

        assertNotEquals(startDeposit, bankAccount.getBalance());
    }

    @Test
    public void cancelTransaction_moneyBack() throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidTransactionOperationException, InvalidMoneyGapException, InvalidDepositInterestRateSelectorException {
        double moneyBefore1 = 3;
        double moneyBefore2 = 2;
        double transactionSum = 1;

        var creditConfiguration = new CreditBankAccountConfiguration(10, 100, 1);
        var depositConfiguration = new DepositBankAccountConfiguration(10, Period.ofDays(1), DepositInterestRateSelectorImpl.getBuilder().addMoneyGap(new MoneyGap(1, 2, 0)).build());
        var debitConfiguration = new DebitBankAccountConfiguration(10, 0.05);
        var configuration = new BankConfiguration(creditConfiguration, debitConfiguration, depositConfiguration);

        var client = new Client("Ivan Ivanov");
        Bank bank = centralBank.createBank("Test Bank", configuration);
        bank.addClient(client);

        BankAccount bankAccount1 = bank.createAccount(new DebitBankAccountFactory(), client, clock);
        BankAccount bankAccount2 = bank.createAccount(new DebitBankAccountFactory(), client, clock);
        bankAccount1.deposit(moneyBefore1);
        bankAccount2.deposit(moneyBefore2);

        Transaction transaction = centralBank.makeTransferTransaction(transactionSum, bankAccount1, bankAccount2);
        transaction.cancel();

        assertEquals(moneyBefore1, bankAccount1.getBalance());
        assertEquals(moneyBefore2, bankAccount2.getBalance());
    }
}
