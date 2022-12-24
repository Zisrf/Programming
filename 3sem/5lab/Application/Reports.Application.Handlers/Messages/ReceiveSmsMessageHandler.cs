using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Common.Exceptions.NotFound;
using Reports.Application.Mapping;
using Reports.Core.Messages;
using Reports.Core.MessageSources;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.Messages.ReceiveSmsMessage;

namespace Reports.Application.Handlers.Messages;

public class ReceiveSmsMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public ReceiveSmsMessageHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var newMessage = new SmsMessage(request.Text, request.SenderPhoneNumber);
        MessageSource messageSource = await _context.Sources.GetEntityByIdAsync(request.SourceId, cancellationToken);

        if (messageSource is not SmsMessageSource smsMessageSource)
            throw new EntityNotFoundException<EmailMessageSource>(request.SourceId);

        smsMessageSource.ReceiveMessage(newMessage);
        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(newMessage.AsDto());
    }
}