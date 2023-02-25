package BanksRemake.Presentation.Handlers;

import BanksRemake.Domain.Services.CentralBank;

import java.util.Scanner;

public class LastHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) {
        System.out.println(String.format("'%s' is invalid request, try 'help' command", request));
    }
}
