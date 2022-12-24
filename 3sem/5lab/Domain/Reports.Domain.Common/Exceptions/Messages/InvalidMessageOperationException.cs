using Reports.Domain.Common.Exceptions;

namespace Reports.Domain.Common.Exceptions.Messages;

public class InvalidMessageOperationException : ReportsDomainException
{
    public InvalidMessageOperationException(string? message)
        : base(message) { }

    public static InvalidMessageOperationException OnIncorrectReceiving()
    {
        return new InvalidMessageOperationException($"Only new messages can be received");
    }

    public static InvalidMessageOperationException OnIncorrectHandling()
    {
        return new InvalidMessageOperationException($"Only received messages can be handled");
    }

    public static InvalidMessageOperationException OnReceiveExistingMessage(Guid sourceId, Guid messageId)
    {
        return new InvalidMessageOperationException($"Message source {sourceId} has already received message {messageId}");
    }
}