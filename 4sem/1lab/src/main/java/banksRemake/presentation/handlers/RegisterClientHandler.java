package banksRemake.presentation.handlers;

import banksRemake.domain.entities.Bank;
import banksRemake.domain.entities.Client;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.services.CentralBank;

import java.util.Scanner;
import java.util.UUID;

public class RegisterClientHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("register client")) {
            System.out.print("Bank id: ");
            var bankId = UUID.fromString(scanner.nextLine());

            System.out.print("Client name: ");
            var clientName = scanner.nextLine();

            Bank bank = centralBank.getBankById(bankId);
            var client = new Client(clientName);
            bank.addClient(client);

            System.out.println(String.format("Client '%s' %s was created", client.getName(), client.getId()));
            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }
}
