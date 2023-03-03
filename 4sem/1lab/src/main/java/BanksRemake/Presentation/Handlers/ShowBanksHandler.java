package banksRemake.presentation.handlers;

import banksRemake.domain.entities.Bank;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.services.CentralBank;

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
