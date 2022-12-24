using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Core.Employees;
using Reports.Core.Messages;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.HandleMessage;

namespace Reports.Application.Handlers;

public class HandleMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public HandleMessageHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Worker worker = await _context.Workers.GetEntityByIdAsync(request.WorkerId, cancellationToken);
        Message message = await _context.Messages.GetEntityByIdAsync(request.MessageId, cancellationToken);

        worker.HandleMessage(message);
        await _context.SaveChangesAsync();

        return new Response();
    }
}