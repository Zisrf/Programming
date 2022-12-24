namespace Reports.Core.Messages;

public partial class MessengerMessage : Message
{
    public MessengerMessage(string text, string senderUsername)
        : base(text)
    {
        ArgumentNullException.ThrowIfNull(senderUsername);

        SenderUsername = senderUsername;
    }

    public string SenderUsername { get; protected init; }
}