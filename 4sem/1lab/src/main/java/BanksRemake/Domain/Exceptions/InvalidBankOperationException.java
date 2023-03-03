package banksRemake.domain.exceptions;

public class InvalidBankOperationException extends BanksRemakeException {
    private InvalidBankOperationException(String message) {
        super(message);
    }

    public static InvalidBankOperationException onAddExistingClient() {
        return new InvalidBankOperationException("Bank already has this client");
    }

    public static InvalidBankOperationException onAddAccountToUnregisteredClient() {
        return new InvalidBankOperationException("Unable to create bank account to unregistered account");
    }
}
