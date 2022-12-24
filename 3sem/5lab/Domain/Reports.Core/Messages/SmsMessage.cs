namespace Reports.Core.Messages;

public partial class SmsMessage : Message
{
    public SmsMessage(string text, string senderPhoneNumber)
        : base(text)
    {
        ArgumentNullException.ThrowIfNull(senderPhoneNumber);

        SenderPhoneNumber = senderPhoneNumber;
    }

    public string SenderPhoneNumber { get; protected init; }
}