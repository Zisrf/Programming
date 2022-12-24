using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Contracts.MessageSources;
using Reports.Application.Dto.MessageSources;

namespace Reports.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageSourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageSourcesController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);

        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("/Create-email-message-source")]
    public async Task<ActionResult<EmailMessageSourceDto>> ReceiveEmailMessageAsync(string email, Guid accountId)
    {
        var command = new CreateEmailMessageSource.Command(email, accountId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.MessageSource);
    }

    [HttpPost("/Create-messenger-message-source")]
    public async Task<ActionResult<MessengerMessageSourceDto>> ReceiveMessengerMessageAsync(string username, Guid accountId)
    {
        var command = new CreateMessengerMessageSource.Command(username, accountId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.MessageSource);
    }

    [HttpPost("/Create-sms-message-source")]
    public async Task<ActionResult<SmsMessageSourceDto>> ReceiveSmsMessageAsync(string phoneNumber, Guid accountId)
    {
        var command = new CreateSmsMessageSource.Command(phoneNumber, accountId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.MessageSource);
    }
}