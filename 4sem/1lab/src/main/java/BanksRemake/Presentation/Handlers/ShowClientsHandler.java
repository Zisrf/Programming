package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Entities.Client;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;

public class ShowClientsHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("show clients")) {
            List<Client> clients = centralBank.getBanks().stream()
                    .flatMap(b -> b.getClients().stream())
                    .distinct()
                    .collect(Collectors.toList());

            for (Client client : clients) {
                System.out.println(String.format(" * Client '%s' %s", client.getName(), client.getId()));
            }

            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }
}
