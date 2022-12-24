using Reports.Core.Models;

namespace Reports.Application.Dto.Messages;

public record EmailMessageDto(Guid Id, string Text, DateTime Date, MessageStatus Status, string SenderEmail, string Topic)
    : MessageDto(Id, Text, Date, Status);