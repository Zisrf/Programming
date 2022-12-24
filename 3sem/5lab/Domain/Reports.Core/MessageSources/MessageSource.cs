using Reports.Core.Messages;
using RichEntity.Annotations;

namespace Reports.Core.MessageSources;

public abstract partial class MessageSource : IEntity<Guid>
{
    protected MessageSource()
        : this(Guid.NewGuid()) { }

    public abstract IReadOnlyCollection<Message> Messages { get; }
}