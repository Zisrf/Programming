using MediatR;
using Reports.Application.Dto.Employees;

namespace Reports.Application.Contracts.Employees;

public static class CreateManager
{
    public record Command(string ManagerName, Guid? ManagerId = null) : IRequest<Response>;

    public record Response(ManagerDto Manager);
}