using MediatR;
using Reports.Application.Dto.Entities;

namespace Reports.Application.Contracts.Entities;

public static class CreateAccount
{
    public record Command(int AccessLevel) : IRequest<Response>;

    public record Response(AccountDto Account);
}