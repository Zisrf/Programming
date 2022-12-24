using MediatR;
using Reports.Application.Dto.Messages;

namespace Reports.Application.Contracts.Messages;

public static class ReceiveSmsMessage
{
    public record Command(string Text, string SenderPhoneNumber, Guid SourceId) : IRequest<Response>;

    public record Response(SmsMessageDto Message);
}