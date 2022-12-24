using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Core.Employees;
using Reports.Core.Entities;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.SignInAccount;

namespace Reports.Application.Handlers;

public class SignInAccountHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public SignInAccountHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Worker worker = await _context.Workers.GetEntityByIdAsync(request.WorkerId, cancellationToken);
        Account account = await _context.Accounts.GetEntityByIdAsync(request.AccountId, cancellationToken);

        worker.SignIn(account);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}