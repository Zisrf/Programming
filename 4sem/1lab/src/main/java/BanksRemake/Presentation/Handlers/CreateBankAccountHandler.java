package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.BankAccounts.BankAccount;
import BanksRemake.Domain.BankAccounts.Factories.BankAccountFactory;
import BanksRemake.Domain.BankAccounts.Factories.CreditBankAccountFactory;
import BanksRemake.Domain.BankAccounts.Factories.DebitBankAccountFactory;
import BanksRemake.Domain.BankAccounts.Factories.DepositBankAccountFactory;
import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

import java.util.Scanner;
import java.util.UUID;

public class CreateBankAccountHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("create bank account")) {
            System.out.print("Bank id: ");
            var bankId = UUID.fromString(scanner.nextLine());
            Bank bank = centralBank.getBankById(bankId);

            System.out.print("Client id: ");
            var clientId = UUID.fromString(scanner.nextLine());
            Client client = bank.getClients().stream()
                    .filter(c -> c.getId().equals(clientId))
                    .findAny()
                    .orElseThrow();

            System.out.println("Account type (debit, credit or deposit): ");
            String type = scanner.nextLine();

            BankAccountFactory factory;
            if (type == "debit") {
                factory = new DebitBankAccountFactory();
            }
            else if (type == "credit") {
                factory = new CreditBankAccountFactory();
            }
            else if (type == "deposit") {
                System.out.println("Deposit sum: ");
                double sum = Double.parseDouble(scanner.nextLine());

                factory = new DepositBankAccountFactory(sum);
            }
            else {
                System.out.println("Invalid account type");
                return;
            }

            BankAccount account = bank.createAccount(factory, client, centralBank.getClock());

            System.out.println(String.format("Bank account %s created", bankId));
            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }
}
