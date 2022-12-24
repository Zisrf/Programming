using Reports.Core.Messages;
using Reports.Domain.Common.Exceptions.Messages;

namespace Reports.Core.MessageSources;

public partial class SmsMessageSource : MessageSource
{
    private readonly HashSet<SmsMessage> _messages;

    public SmsMessageSource(string phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(phoneNumber);

        PhoneNumber = phoneNumber;

        _messages = new HashSet<SmsMessage>();
    }

    public string PhoneNumber { get; protected init; }
    public override IReadOnlyCollection<Message> Messages => _messages;

    public void ReceiveMessage(SmsMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        message.Receive();

        if (!_messages.Add(message))
            throw InvalidMessageOperationException.OnReceiveExistingMessage(Id, message.Id);
    }
}
