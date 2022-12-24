﻿using MediatR;
using Reports.Abstractions.DataAccess;
using Reports.Application.Mapping;
using Reports.Core.Entities;
using Reports.Core.MessageSources;
using Reports.DataAccess.Extensions;
using static Reports.Application.Contracts.MessageSources.CreateMessengerMessageSource;

namespace Reports.Application.Handlers.MessageSources;

public class CreateMessengerMessageSourceHandler : IRequestHandler<Command, Response>
{
    private readonly IReportsDatabaseContext _context;

    public CreateMessengerMessageSourceHandler(IReportsDatabaseContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var newMessageSource = new MessengerMessageSource(request.Username);
        Account account = await _context.Accounts.GetEntityByIdAsync(request.AccountId, cancellationToken);

        account.AddMessageSource(newMessageSource);
        _context.Sources.Add(newMessageSource);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(newMessageSource.AsDto());
    }
}