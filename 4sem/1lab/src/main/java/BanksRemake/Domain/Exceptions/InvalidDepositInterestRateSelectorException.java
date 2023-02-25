package BanksRemake.Domain.Exceptions;

public class InvalidDepositInterestRateSelectorException extends BanksRemakeException {
    public InvalidDepositInterestRateSelectorException(String message) {
        super(message);
    }

    public static InvalidDepositInterestRateSelectorException onGapsIntersection() {
        return new InvalidDepositInterestRateSelectorException("Money gaps must not intersect");
    }

    public static InvalidDepositInterestRateSelectorException onEmptyGapsList() {
        return new InvalidDepositInterestRateSelectorException("There must be at least one money gap");
    }
}
