using Reports.Application.Dto.Messages;
using Reports.Core.Messages;

namespace Reports.Application.Mapping;

public static class MessagesMapping
{
    public static EmailMessageDto AsDto(this EmailMessage message)
    {
        return new EmailMessageDto(
            message.Id, 
            message.Text, 
            message.Date, 
            message.Status, 
            message.SenderEmail,
            message.Topic
            );
    }

    public static MessengerMessageDto AsDto(this MessengerMessage message)
    {
        return new MessengerMessageDto(
            message.Id,
            message.Text,
            message.Date,
            message.Status,
            message.SenderUsername
        );
    }

    public static SmsMessageDto AsDto(this SmsMessage message)
    {
        return new SmsMessageDto(
            message.Id,
            message.Text,
            message.Date,
            message.Status,
            message.SenderPhoneNumber
        );
    }
}