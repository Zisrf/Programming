using Reports.Core.Messages;
using Reports.Domain.Common.Exceptions.Messages;

namespace Reports.Core.MessageSources;

public partial class MessengerMessageSource : MessageSource
{
    private readonly HashSet<MessengerMessage> _messages;

    public MessengerMessageSource(string username)
    {
        ArgumentNullException.ThrowIfNull(username);

        Username = username;

        _messages = new HashSet<MessengerMessage>();
    }

    public string Username { get; protected init; }
    public override IReadOnlyCollection<Message> Messages => _messages;

    public void ReceiveMessage(MessengerMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        message.Receive();

        if (!_messages.Add(message))
            throw InvalidMessageOperationException.OnReceiveExistingMessage(Id, message.Id);
    }
}
