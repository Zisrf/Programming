using Reports.Core.Messages;
using Reports.Core.Models;

namespace Reports.Core.Extensions;

public static class MessagesExtensions
{
    public static IEnumerable<Message> GetNew(this IEnumerable<Message> messages)
    {
        return messages.Where(m => m.Status is MessageStatus.New);
    }

    public static IEnumerable<Message> GetReceived(this IEnumerable<Message> messages)
    {
        return messages.Where(m => m.Status is MessageStatus.Received);
    }

    public static IEnumerable<Message> GetHandled(this IEnumerable<Message> messages)
    {
        return messages.Where(m => m.Status is MessageStatus.Handled);
    }
}
