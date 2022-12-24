using MediatR;
using Reports.Application.Dto.MessageSources;

namespace Reports.Application.Contracts.MessageSources;

public static class CreateEmailMessageSource
{
    public record Command(string Email, Guid AccountId) : IRequest<Response>;

    public record Response(EmailMessageSourceDto MessageSource);
}