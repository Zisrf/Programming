namespace Banks.Exceptions;

public class InvalidMoneyGapException : BanksDomainException
{
    private InvalidMoneyGapException(string? message)
        : base(message) { }

    public static InvalidMoneyGapException OnInvalidMoneyAmount()
        => new InvalidMoneyGapException("Money amount must be positive");

    public static InvalidMoneyGapException OnInvalidInterestRate()
        => new InvalidMoneyGapException($"Interest rate must be in [0; 1]");

    public static InvalidMoneyGapException OnFromSumGreaterThanTo()
        => new InvalidMoneyGapException($"'From' sum must be lower than 'To' sum");
}