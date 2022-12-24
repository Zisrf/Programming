using MediatR;
using Reports.Application.Dto.Messages;

namespace Reports.Application.Contracts.Messages;

public static class ReceiveMessengerMessage
{
    public record Command(string Text, string SenderUsername, Guid SourceId) : IRequest<Response>;

    public record Response(MessengerMessageDto Message);
}