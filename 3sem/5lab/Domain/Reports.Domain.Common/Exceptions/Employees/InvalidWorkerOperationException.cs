namespace Reports.Domain.Common.Exceptions.Employees;

public class InvalidWorkerOperationException : ReportsDomainException
{
    private InvalidWorkerOperationException(string? message)
        : base(message) { }

    public static InvalidWorkerOperationException OnLackOfAccessLevel()
    {
        return new InvalidWorkerOperationException($"Unable to sign in account with higher access level");
    }

    public static InvalidWorkerOperationException OnHandlingIncorrectMessage()
    {
        return new InvalidWorkerOperationException($"Unable to handle message that doesn't contains in account");
    }
}
