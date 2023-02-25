package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

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
