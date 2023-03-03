package banksRemake.presentation;

import banksRemake.domain.clocks.FrozenTimeClock;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.services.CentralBank;
import banksRemake.presentation.handlers.*;

import java.time.Instant;
import java.util.Date;
import java.util.Scanner;

public class Program {
    public static void main(String[] args) {
        try {
            startApp();
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    private static void startApp() throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        var clock = new FrozenTimeClock(Date.from(Instant.now()));
        var centralBank = new CentralBank(clock);
        var in = new Scanner(System.in);

        Handler handler = new EmptyHandler();
        initHandlers(handler);

        printGreetings();

        while (in.hasNext()) {
            String request = in.nextLine();
            handler.handle(request, centralBank, in);
        }
    }

    private static void printGreetings()
    {
        System.out.println("----- Welcome to Banks remake -----");
        System.out.println();
        System.out.println("Enter a command:");
    }

    private static void initHandlers(Handler handler) {
        handler.setNext(new HelpHandler())
                .setNext(new ExitHandler())
                .setNext(new CreateBankHandler())
                .setNext(new ShowBanksHandler())
                .setNext(new RegisterClientHandler())
                .setNext(new ShowClientsHandler())
                .setNext(new CreateBankAccountHandler())
                .setNext(new LastHandler());
    }
}
