package banksRemake.domain.exceptions;

public class InvalidBankAccountOperationException extends BanksRemakeException {
    private InvalidBankAccountOperationException(String message) {
        super(message);
    }

    public static InvalidBankAccountOperationException onInvalidMoneyAmount() {
        return new InvalidBankAccountOperationException("Money amount must be positive");
    }

    public static InvalidBankAccountOperationException onInvalidInterestRate() {
        return new InvalidBankAccountOperationException("Interest rate must be in [0; 1]");
    }

    public static InvalidBankAccountOperationException onInvalidCreditLimit() {
        return new InvalidBankAccountOperationException("Credit limit must be positive");
    }

    public static InvalidBankAccountOperationException onInvalidCommission() {
        return new InvalidBankAccountOperationException("Commission must be positive");
    }

    public static InvalidBankAccountOperationException onShortageOfMoney(double sum, double balance) {
        return new InvalidBankAccountOperationException(String.format("Unable to withdraw %1, account has only %2", sum, balance));
    }
}
