package banksRemake.domain.exceptions;

public class InvalidTransactionOperationException extends BanksRemakeException {
    private InvalidTransactionOperationException(String message) {
        super(message);
    }

    public static InvalidTransactionOperationException onTransactionToSameAccount() {
        return new InvalidTransactionOperationException("Unable to make transaction to the same bank account");
    }

    public static InvalidTransactionOperationException onIncorrectExecuting() {
        return new InvalidTransactionOperationException("Executed can be only transactions with status 'Created'");
    }

    public static InvalidTransactionOperationException onIncorrectCanceling() {
        return new InvalidTransactionOperationException("Canceled can be only transactions with status 'Executed'");
    }

    public static InvalidTransactionOperationException onInvalidTransaction() {
        return new InvalidTransactionOperationException("Unable to make or cancel transaction");
    }
}
