using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reports.Application.Contracts.Employees;
using Reports.Application.Contracts.Messages;
using Reports.Application.Dto.Employees;
using Reports.Application.Dto.Messages;

namespace Reports.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);

        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("/Create-email-message")]
    public async Task<ActionResult<EmailMessageDto>> ReceiveEmailMessageAsync(string text, string topic, string senderEmail, Guid sourceId)
    {
        var command = new ReceiveEmailMessage.Command(text, topic, senderEmail, sourceId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("/Create-messenger-message")]
    public async Task<ActionResult<MessengerMessageDto>> ReceiveMessengerMessageAsync(string text, string username, Guid sourceId)
    {
        var command = new ReceiveMessengerMessage.Command(text, username, sourceId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }

    [HttpPost("/Create-sms-message")]
    public async Task<ActionResult<SmsMessageDto>> ReceiveSmsMessageAsync(string text, string senderPhoneNumber, Guid sourceId)
    {
        var command = new ReceiveSmsMessage.Command(text, senderPhoneNumber, sourceId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Message);
    }
}