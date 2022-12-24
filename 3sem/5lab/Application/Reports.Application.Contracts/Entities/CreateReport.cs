using MediatR;
using Reports.Application.Dto.Entities;

namespace Reports.Application.Contracts.Entities;

public static class CreateReport
{
    public record Command(Guid ManagerId) : IRequest<Response>;

    public record Response(ReportDto Report);
}