using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Contracts.Employees;
using Reports.Application.Contracts.Entities;
using Reports.Application.Dto.Entities;
using Reports.Core.Employees;
using Reports.Core.ValueObjects;
using System.Xml.Linq;
using Reports.Application.Contracts;

namespace Reports.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);

        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("/Create-account")]
    public async Task<ActionResult<AccountDto>> CreateWorkerAsync(int accessLevel)
    {
        var command = new CreateAccount.Command(accessLevel);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Account);
    }

    [HttpPost("/Sign-worker-in-account")]
    public async Task<ActionResult> LogInAccountAsync(Guid workerId, Guid accountId)
    {
        var command = new SignInAccount.Command(workerId, accountId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}