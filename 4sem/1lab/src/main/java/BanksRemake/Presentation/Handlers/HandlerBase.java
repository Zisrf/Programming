package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Exceptions.InvalidBankAccountOperationException;
import BanksRemake.Domain.Exceptions.InvalidBankOperationException;
import BanksRemake.Domain.Exceptions.InvalidDepositInterestRateSelectorException;
import BanksRemake.Domain.Exceptions.InvalidMoneyGapException;
import BanksRemake.Domain.Services.CentralBank;

import java.util.Scanner;

public abstract class HandlerBase implements Handler {
    private Handler next;

    protected HandlerBase() {
        next = null;
    }

    @Override
    public Handler getNext() {
        return next;
    }

    @Override
    public Handler setNext(Handler next) {
        this.next = next;
        return next;
    }

    @Override
    public abstract void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException;
}
