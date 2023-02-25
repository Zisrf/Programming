package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Entities.Bank;
import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

import java.util.Scanner;

public class ShowBanksHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("show banks")) {
            for (Bank bank : centralBank.getBanks()) {
                System.out.println(String.format(" * Bank '%s' %s", bank.getName(), bank.getId()));
            }

            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }
}
