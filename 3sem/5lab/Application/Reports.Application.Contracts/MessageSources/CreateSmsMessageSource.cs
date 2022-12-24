using MediatR;
using Reports.Application.Dto.MessageSources;

namespace Reports.Application.Contracts.MessageSources;

public static class CreateSmsMessageSource
{
    public record Command(string PhoneNumber, Guid AccountId) : IRequest<Response>;

    public record Response(SmsMessageSourceDto MessageSource);
}