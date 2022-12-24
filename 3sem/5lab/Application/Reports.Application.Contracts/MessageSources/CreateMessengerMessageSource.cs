using MediatR;
using Reports.Application.Dto.MessageSources;

namespace Reports.Application.Contracts.MessageSources;

public static class CreateMessengerMessageSource
{
    public record Command(string Username, Guid AccountId) : IRequest<Response>;

    public record Response(MessengerMessageSourceDto MessageSource);
}