package banksRemake.presentation.handlers;

import banksRemake.domain.services.CentralBank;

import java.util.Scanner;

public class LastHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) {
        System.out.println(String.format("'%s' is invalid request, try 'help' command", request));
    }
}
