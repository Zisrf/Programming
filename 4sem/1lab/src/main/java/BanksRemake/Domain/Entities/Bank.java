package BanksRemake.Domain.Entities;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.Factories.BankAccountFactory;
import BanksRemake.Domain.Clocks.Clock;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Models.BankConfigurations.BankConfiguration;
import BanksRemake.Domain.Services.CentralBank;
import lombok.Getter;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.UUID;

/**
 * Bank serving clients.
 */
public class Bank {
    private final ArrayList<Client> clients;
    private final ArrayList<BankAccount> accounts;

    @Getter
    private final UUID id;
    @Getter
    private final String name;
    @Getter
    private final BankConfiguration configuration;
    @Getter
    private final CentralBank centralBank;

    public Bank(String name, BankConfiguration configuration, CentralBank centralBank) {
        this.name = name;
        this.configuration = configuration;
        this.centralBank = centralBank;

        this.id = UUID.randomUUID();
        this.clients = new ArrayList<>();
        this.accounts = new ArrayList<>();
    }

    public List<Client> getClients() {
        return Collections.unmodifiableList(clients);
    }

    public List<BankAccount> getAccounts() {
        return Collections.unmodifiableList(accounts);
    }

    /**
     * Adding a client to the bank.
     * @param client unregistered client
     */
    public void addClient(Client client) throws InvalidBankOperationException {
        if (clients.contains(client)) {
            throw InvalidBankOperationException.onAddExistingClient();
        }

        clients.add(client);
    }

    /**
     * Creating a bank account for a client.
     * @param factory abstract factory that creates different types of bank accounts.
     * @param client registered client.
     * @param clock a clock that notifies accounts of the arrival of a new day or month.
     * @return created bank account.
     */
    public BankAccount createAccount(BankAccountFactory factory, Client client, Clock clock) throws InvalidBankOperationException, InvalidBankAccountOperationException {
        if (!clients.contains(client)) {
            throw InvalidBankOperationException.onAddAccountToUnregisteredClient();
        }

        BankAccount account = factory.create(this, client, clock);
        accounts.add(account);

        return account;
    }
}
