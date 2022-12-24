using MediatR;
using Reports.Application.Dto.Employees;

namespace Reports.Application.Contracts.Employees;

public static class CreateWorker
{
    public record Command(string WorkerName, int WorkerAccessLevel, Guid ManagerId) : IRequest<Response>;

    public record Response(WorkerDto Worker);
}