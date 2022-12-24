using MediatR;

namespace Reports.Application.Contracts;

public static class HandleMessage
{
    public record Command(Guid WorkerId, Guid MessageId) : IRequest<Response>;

    public record Response;
}