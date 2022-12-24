using Reports.Core.Models;

namespace Reports.Application.Dto.Messages;

public record SmsMessageDto(Guid Id, string Text, DateTime Date, MessageStatus Status, string SenderPhoneNumber)
    : MessageDto(Id, Text, Date, Status);