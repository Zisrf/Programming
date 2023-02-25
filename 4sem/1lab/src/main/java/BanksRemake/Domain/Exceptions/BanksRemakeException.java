package BanksRemake.Domain.Exceptions;

/**
 * Basic exception for domain entities.
 */
public abstract class BanksRemakeException extends Exception {
    protected BanksRemakeException(String message) {
        super(message);
    }
}
