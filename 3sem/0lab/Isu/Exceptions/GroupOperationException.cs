namespace Isu.Exceptions;

public class GroupOperationException : IsuDomainException
{
    public GroupOperationException(string? message)
        : base(message)
    { }
}
