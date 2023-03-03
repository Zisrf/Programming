package banksRemake.presentation.handlers;

import banksRemake.domain.bankAccounts.BankAccount;
import banksRemake.domain.bankAccounts.Factories.BankAccountFactory;
import banksRemake.domain.bankAccounts.Factories.CreditBankAccountFactory;
import banksRemake.domain.bankAccounts.Factories.DebitBankAccountFactory;
import banksRemake.domain.bankAccounts.Factories.DepositBankAccountFactory;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.services.CentralBank;

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
