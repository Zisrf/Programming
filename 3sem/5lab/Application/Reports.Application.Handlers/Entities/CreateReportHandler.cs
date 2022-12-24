using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Mapping;
using Reports.Core.Employees;
using Reports.Core.Entities;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.Entities.CreateReport;

namespace Reports.Application.Handlers.Entities;

public class CreateReportHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public CreateReportHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Manager manager = await _context.Managers.GetEntityByIdAsync(request.ManagerId, cancellationToken);

        Report report = manager.GetReport();
        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(report.AsDto());
    }
}