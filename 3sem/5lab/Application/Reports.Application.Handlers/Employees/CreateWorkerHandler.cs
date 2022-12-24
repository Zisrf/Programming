using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Mapping;
using Reports.Core.Employees;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.Employees.CreateWorker;

namespace Reports.Application.Handlers.Employees;

public class CreateWorkerHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public CreateWorkerHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var newWorker = new Worker(request.WorkerName, request.WorkerAccessLevel);
        Manager manager = await _context.Managers.GetEntityByIdAsync(request.ManagerId, cancellationToken);

        manager.AddSubordinate(newWorker);
        _context.Workers.Add(newWorker);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(newWorker.AsDto());
    }
}