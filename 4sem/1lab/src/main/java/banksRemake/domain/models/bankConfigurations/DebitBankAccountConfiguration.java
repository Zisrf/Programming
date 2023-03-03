package banksRemake.domain.models.bankConfigurations;

/**
 * A set of parameters characterizing the debit account.
 * @param unverifiedLimit maximum transaction amount for an unverified user.
 * @param interestRate interest accrued every month.
 */
public record DebitBankAccountConfiguration(double unverifiedLimit,
                                            double interestRate) {
}
