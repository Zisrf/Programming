namespace Reports.Core.Messages;

public partial class EmailMessage : Message
{
    public EmailMessage(string text, string topic, string senderEmail)
        : base(text)
    {
        ArgumentNullException.ThrowIfNull(senderEmail);
        ArgumentNullException.ThrowIfNull(topic);

        Topic = topic;
        SenderEmail = senderEmail;
    }

    public string Topic { get; protected init; }
    public string SenderEmail { get; protected init; }
}