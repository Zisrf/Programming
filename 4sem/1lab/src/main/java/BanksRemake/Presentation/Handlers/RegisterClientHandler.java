package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

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
