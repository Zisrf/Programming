using MediatR;
using Reports.Application.Dto.Messages;

namespace Reports.Application.Contracts.Messages;

public static class ReceiveEmailMessage
{
    public record Command(string Text, string Topic, string SenderEmail, Guid SourceId) : IRequest<Response>;

    public record Response(EmailMessageDto Message);
}