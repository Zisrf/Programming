using MediatR;

namespace Reports.Application.Contracts;

public static class SignInAccount
{
    public record Command(Guid WorkerId, Guid AccountId) : IRequest<Response>;

    public record Response;
}