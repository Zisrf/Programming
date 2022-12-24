using Reports.Core.Messages;
using Reports.Domain.Common.Exceptions.Messages;

namespace Reports.Core.MessageSources;

public partial class EmailMessageSource : MessageSource
{
    private readonly HashSet<EmailMessage> _messages;

    public EmailMessageSource(string email)
    {
        ArgumentNullException.ThrowIfNull(email);

        Email = email;

        _messages = new HashSet<EmailMessage>();
    }

    public string Email { get; protected init; }
    public override IReadOnlyCollection<Message> Messages => _messages;

    public void ReceiveMessage(EmailMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        message.Receive();

        if (!_messages.Add(message))
            throw InvalidMessageOperationException.OnReceiveExistingMessage(Id, message.Id);
    }
}
