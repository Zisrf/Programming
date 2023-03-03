package banksRemake.presentation.handlers;

import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.services.CentralBank;

import java.util.Scanner;

public class HelpHandler extends HandlerBase {
    private final String helpMessage = "Commands: \n" +
            " * Help\n" +
            " * Exit\n" +
            " * Register client\n" +
            " * Show clients\n" +
            " * Create bank\n" +
            " * Show banks\n" +
            " * Create bank account";
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("help")) {
            System.out.println(helpMessage);
            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }
}
