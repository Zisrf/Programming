using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Contracts;
using Reports.Application.Contracts.Employees;
using Reports.Application.Contracts.Entities;
using Reports.Application.Dto.Employees;
using Reports.Application.Dto.Entities;

namespace Reports.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);

        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("/Create-manager")]
    public async Task<ActionResult<WorkerDto>> CreateManagerAsync(string name, Guid? managerId)
    {
        var command = new CreateManager.Command(name, managerId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Manager);
    }

    [HttpPost("/Create-worker")]
    public async Task<ActionResult<WorkerDto>> CreateWorkerAsync(string name, int accessLevel, Guid managerId)
    {
        var command = new CreateWorker.Command(name, accessLevel, managerId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Worker);
    }

    [HttpPost("/Handle-message")]
    public async Task<ActionResult> HandleMessageAsync(Guid workerId, Guid messageId)
    {
        var command = new HandleMessage.Command(workerId, messageId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }

    [HttpPost("/Create-report")]
    public async Task<ActionResult<AccountDto>> CreateReportAsync(Guid managerId)
    {
        var command = new CreateReport.Command(managerId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Report);
    }
}