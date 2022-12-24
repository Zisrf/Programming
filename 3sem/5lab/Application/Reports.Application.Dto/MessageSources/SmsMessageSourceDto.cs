namespace Reports.Application.Dto.MessageSources;

public record SmsMessageSourceDto(Guid Id, string PhoneNumber)
    : MessageSourceDto(Id);