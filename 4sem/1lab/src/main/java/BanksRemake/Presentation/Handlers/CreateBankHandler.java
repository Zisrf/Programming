package banksRemake.presentation.handlers;

import banksRemake.domain.depositInterestRateSelectors.DepositInterestRateSelectorImpl;
import banksRemake.domain.entities.Bank;
import banksRemake.domain.exceptions.InvalidBankAccountOperationException;
import banksRemake.domain.exceptions.InvalidBankOperationException;
import banksRemake.domain.exceptions.InvalidDepositInterestRateSelectorException;
import banksRemake.domain.exceptions.InvalidMoneyGapException;
import banksRemake.domain.models.bankConfigurations.BankConfiguration;
import banksRemake.domain.models.bankConfigurations.CreditBankAccountConfiguration;
import banksRemake.domain.models.bankConfigurations.DebitBankAccountConfiguration;
import banksRemake.domain.models.bankConfigurations.DepositBankAccountConfiguration;
import banksRemake.domain.models.MoneyGap;
import banksRemake.domain.services.CentralBank;

import java.time.Period;
import java.util.Scanner;

public class CreateBankHandler extends HandlerBase {
    @Override
    public void handle(String request, CentralBank centralBank, Scanner scanner) throws InvalidBankOperationException, InvalidBankAccountOperationException, InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        if (request.toLowerCase().equals("create bank")) {
            String name = ReadBankName(scanner);
            BankConfiguration configuration = ReadBankConfiguration(scanner);

            Bank bank = centralBank.createBank(name, configuration);
            System.out.println(String.format("Bank '%s' %s created!", bank.getName(), bank.getId()));

            return;
        }

        if (getNext() != null) {
            getNext().handle(request, centralBank, scanner);
        }
    }

    private static String ReadBankName(Scanner scanner)
    {
        System.out.print("Name: ");
        return scanner.nextLine();
    }

    private static BankConfiguration ReadBankConfiguration(Scanner scanner) throws InvalidDepositInterestRateSelectorException, InvalidMoneyGapException {
        return new BankConfiguration(
                ReadCreditConfiguration(scanner),
                ReadDebitConfiguration(scanner),
                ReadDepositConfiguration(scanner));
    }

    private static DebitBankAccountConfiguration ReadDebitConfiguration(Scanner scanner)
    {
        System.out.print("Debit unverified limit: ");
        double unverifiedLimit = Double.parseDouble(scanner.nextLine());

        System.out.print("Debit interest rate: ");
        double interestRate = Double.parseDouble(scanner.nextLine());

        return new DebitBankAccountConfiguration(unverifiedLimit, interestRate);
    }

    private static CreditBankAccountConfiguration ReadCreditConfiguration(Scanner scanner)
    {
        System.out.print("Credit unverified limit: ");
        double unverifiedLimit = Double.parseDouble(scanner.nextLine());

        System.out.print("Credit limit: ");
        double creditLimit = Double.parseDouble(scanner.nextLine());

        System.out.print("Credit commission: ");
        double commission = Double.parseDouble(scanner.nextLine());

        return new CreditBankAccountConfiguration(unverifiedLimit, creditLimit, commission);
    }

    private static DepositBankAccountConfiguration ReadDepositConfiguration(Scanner scanner) throws InvalidMoneyGapException, InvalidDepositInterestRateSelectorException {
        System.out.print("Deposit unverified limit: ");
        double unverifiedLimit = Double.parseDouble(scanner.nextLine());

        System.out.print("Deposit duration: ");
        var duration = Period.parse(scanner.nextLine());

        System.out.print("Deposit gaps count: ");
        int gapsCount = Integer.parseInt(scanner.nextLine());
        var builder = DepositInterestRateSelectorImpl.getBuilder();
        for (int i = 0; i < gapsCount; ++i)
        {
            System.out.print(String.format("%s gap's from: ", i + 1));
            double from = Double.parseDouble(scanner.nextLine());

            System.out.print(String.format("%s gap's to: ", i + 1));
            double to = Double.parseDouble(scanner.nextLine());

            System.out.print(String.format("%s gap's interest rate: ", i + 1));
            double interestRate = Double.parseDouble(scanner.nextLine());

            builder.addMoneyGap(new MoneyGap(from, to, interestRate));
        }

        return new DepositBankAccountConfiguration(unverifiedLimit, duration, builder.build());
    }
}
