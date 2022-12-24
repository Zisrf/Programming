using Reports.Domain.Common.Exceptions;

namespace Reports.Domain.Common.Exceptions.Employees;

public class InvalidManagerOperationException : ReportsDomainException
{
    private InvalidManagerOperationException(string? message)
        : base(message) { }

    public static InvalidManagerOperationException OnAddHimselfToSubordinates()
    {
        return new InvalidManagerOperationException($"Manager can't manage himself");
    }

    public static InvalidManagerOperationException OnAddExistingSubordinate()
    {
        return new InvalidManagerOperationException($"Unable to add existing subordinate");
    }

    public static InvalidManagerOperationException OnRemoveNonExistentSubordinate()
    {
        return new InvalidManagerOperationException($"Unable to remove non existent subordinate");
    }
}