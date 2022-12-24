namespace Banks.Models.BankConfigurations;

public record BankConfiguration
{
    public BankConfiguration(
        CreditBankAccountConfiguration creditConfiguration,
        DebitBankAccountConfiguration debitConfiguration,
        DepositBankAccountConfiguration depositConfiguration)
    {
        ArgumentNullException.ThrowIfNull(creditConfiguration);
        ArgumentNullException.ThrowIfNull(debitConfiguration);
        ArgumentNullException.ThrowIfNull(depositConfiguration);

        CreditConfiguration = creditConfiguration;
        DebitConfiguration = debitConfiguration;
        DepositConfiguration = depositConfiguration;
    }

    public CreditBankAccountConfiguration CreditConfiguration { get; }
    public DebitBankAccountConfiguration DebitConfiguration { get; }
    public DepositBankAccountConfiguration DepositConfiguration { get; }
}