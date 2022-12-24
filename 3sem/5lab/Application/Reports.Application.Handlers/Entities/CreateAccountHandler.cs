using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Mapping;
using Reports.Core.Entities;
using static Reports.Application.Contracts.Entities.CreateAccount;

namespace Reports.Application.Handlers.Entities;

public class CreateAccountHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public CreateAccountHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var newAccount = new Account(request.AccessLevel);

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(newAccount.AsDto());
    }
}