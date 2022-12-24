using Reports.Core.Models;

namespace Reports.Application.Dto.Messages;

public abstract record MessageDto(Guid Id, string Text, DateTime Date, MessageStatus Status);