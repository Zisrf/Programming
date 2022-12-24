using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Mapping;
using Reports.Core.Employees;
using static Reports.Application.Contracts.Employees.CreateManager;

namespace Reports.Application.Handlers.Employees;

public class CreateManagerHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public CreateManagerHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var newManager = new Manager(request.ManagerName);
        Manager? manager = await _context.Managers.FindAsync(request.ManagerId, cancellationToken);

        manager?.AddSubordinate(newManager);
        _context.Managers.Add(newManager);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(newManager.AsDto());
    }
}