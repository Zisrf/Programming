using Reports.Core.Models;

namespace Reports.Application.Dto.Messages;

public record MessengerMessageDto(Guid Id, string Text, DateTime Date, MessageStatus Status, string SenderUsername)
    : MessageDto(Id, Text, Date, Status);