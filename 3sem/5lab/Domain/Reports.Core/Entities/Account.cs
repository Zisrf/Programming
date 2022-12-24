using Reports.Core.Extensions;
using Reports.Core.Messages;
using Reports.Core.MessageSources;
using Reports.Core.ValueObjects;
using Reports.Domain.Common.Exceptions.Accounts;
using RichEntity.Annotations;

namespace Reports.Core.Entities;

public partial class Account : IEntity<Guid>
{
    private readonly HashSet<MessageSource> _availableSources;

    public Account(AccessLevel accessLevel)
        : this(Guid.NewGuid())
    {
        AccessLevel = accessLevel;

        _availableSources = new HashSet<MessageSource>();
    }

    public AccessLevel AccessLevel { get; protected init; }
    public virtual IReadOnlyCollection<MessageSource> AvailableSources => _availableSources;

    public IEnumerable<Message> GetUnhandledMessages()
    {
        return _availableSources
            .SelectMany(s => s.Messages)
            .GetNew();
    }

    public void AddMessageSource(MessageSource source)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (!_availableSources.Add(source))
            throw InvalidAccountOperationException.OnAddExistingMessageSource(Id, source.Id);
    }
}
