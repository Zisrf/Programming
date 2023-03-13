package banksRemake.tests;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.bankAccounts.Factories.DebitBankAccountFactory;
import banksRemake.domain.clocks.FrozenTimeClock;
import banksRemake.domain.depositInterestRateSelectors.DepositInterestRateSelectorImpl;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.*;
import banksRemake.domain.models.bankConfigurations.BankConfiguration;
import banksRemake.domain.models.bankConfigurations.CreditBankAccountConfiguration;
import banksRemake.domain.models.bankConfigurations.DebitBankAccountConfiguration;
import banksRemake.domain.models.bankConfigurations.DepositBankAccountConfiguration;
import banksRemake.domain.models.MoneyGap;
import banksRemake.domain.services.CentralBank;
import banksRemake.domain.transactions.Transaction;
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
    public void whenTimePassed_thenInterestAccrues() throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidMoneyGapException, InvalidDepositInterestRateSelectorException {
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
    public void whenCancelTransaction_thenMoneyBack() throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidTransactionOperationException, InvalidMoneyGapException, InvalidDepositInterestRateSelectorException {
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
