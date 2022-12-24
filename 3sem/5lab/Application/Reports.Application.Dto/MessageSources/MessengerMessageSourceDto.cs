namespace Reports.Application.Dto.MessageSources;

public record MessengerMessageSourceDto(Guid Id, string Username)
    : MessageSourceDto(Id);