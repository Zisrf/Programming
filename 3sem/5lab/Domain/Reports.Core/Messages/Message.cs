using Reports.Core.Models;
using Reports.Domain.Common.Exceptions.Messages;
using RichEntity.Annotations;

namespace Reports.Core.Messages;

public abstract partial class Message : IEntity<Guid>
{
    protected Message(string text)
        : this(Guid.NewGuid())
    {
        ArgumentNullException.ThrowIfNull(text);

        Status = MessageStatus.New;

        Text = text;

        Date = DateTime.Now;
    }

    public string Text { get; protected init; }
    public DateTime Date { get; protected init; }
    public MessageStatus Status { get; private set; }

    public void Receive()
    {
        if (Status is not MessageStatus.New)
            throw InvalidMessageOperationException.OnIncorrectReceiving();

        Status = MessageStatus.Received;
    }

    public void Handle()
    {
        if (Status is not MessageStatus.Received)
            throw InvalidMessageOperationException.OnIncorrectHandling();

        Status = MessageStatus.Handled;
    }
}