package banksRemake.domain.services;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.clocks.Clock;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidTransactionOperationException;
import banksRemake.domain.models.bankConfigurations.BankConfiguration;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.transactions.DepositTransaction;
import banksRemake.domain.transactions.Transaction;
import banksRemake.domain.transactions.TransferTransaction;
import banksRemake.domain.transactions.WithdrawTransaction;
import lombok.Getter;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.UUID;

/**
 * The main bank that manages other banks and carries out transactions.
 */
public class CentralBank {
    private final ArrayList<Transaction> transactions;
    private final ArrayList<Bank> banks;

    @Getter
    private final Clock clock;

    public CentralBank(Clock clock) {
        this.clock = clock;

        this.transactions = new ArrayList<Transaction>();
        this.banks = new ArrayList<Bank>();
    }

    /**
     * @return all banks under the control of the central bank.
     */
    public List<Bank> getBanks() {
        return Collections.unmodifiableList(banks);
    }

    /**
     * @return all transactions made by the central bank.
     */
    public List<Transaction> getTransactions() {
        return Collections.unmodifiableList(transactions);
    }

    /**
     * Bank search by id.
     * @param id bank identifier.
     * @return found bank or throws an exception.
     */
    public Bank getBankById(UUID id) {
        return banks.stream()
                .filter(b -> b.getId().equals(id))
                .findAny()
                .orElseThrow();
    }

    /**
     * Creation of a new bank.
     * @param name the name of the new bank.
     * @param configuration a set of parameters characterizing the bank products.
     * @return created bank
     */
    public Bank createBank(String name, BankConfiguration configuration) {
        var bank = new Bank(name, configuration, this);
        banks.add(bank);
        return bank;
    }

    /**
     * Transaction search by id.
     * @param id transaction identifier.
     * @return found transaction or throws an exception.
     */
    public Transaction getTransactionById(UUID id) {
        return transactions.stream()
                .filter(t -> t.getId().equals(id))
                .findAny()
                .orElseThrow();
    }

    /**
     * @param sum The amount of money to deposit on the account.
     * @param account sum receiver.
     * @return created transaction.
     */
    public Transaction makeDepositTransaction(double sum, BankAccount account) throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        var transaction = new DepositTransaction(sum, account);

        transaction.execute();
        transactions.add(transaction);

        return transaction;
    }

    /**
     * @param sum the amount of money to withdraw from the account.
     * @param account sum receiver.
     * @return created transaction.
     */
    public Transaction makeWithdrawTransaction(double sum, BankAccount account) throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        var transaction = new WithdrawTransaction(sum, account);

        transaction.execute();
        transactions.add(transaction);

        return transaction;
    }

    /**
     * @param sum the amount of money to transfer.
     * @param sender sum sender.
     * @param receiver sum receiver.
     * @return created transaction.
     */
    public Transaction makeTransferTransaction(double sum, BankAccount sender, BankAccount receiver) throws InvalidTransactionOperationException, InvalidBankAccountOperationException {
        var transaction = new TransferTransaction(sum, sender, receiver);

        transaction.execute();
        transactions.add(transaction);

        return transaction;
    }
}
