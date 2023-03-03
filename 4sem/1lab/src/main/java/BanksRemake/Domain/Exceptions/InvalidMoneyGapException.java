package banksRemake.domain.exceptions;

public class InvalidMoneyGapException extends BanksRemakeException {
    private InvalidMoneyGapException(String message) {
        super(message);
    }

    public static InvalidMoneyGapException onInvalidMoneyAmount() {
        return new InvalidMoneyGapException("Money amount must be positive");
    }

    public static InvalidMoneyGapException onInvalidInterestRate() {
        return new InvalidMoneyGapException("Interest rate must be in [0; 1]");
    }

    public static InvalidMoneyGapException onFromSumGreaterThanTo(){
        return new InvalidMoneyGapException("'From' sum must be lower than 'To' sum");
    }
}