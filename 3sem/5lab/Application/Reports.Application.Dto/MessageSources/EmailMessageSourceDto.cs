namespace Reports.Application.Dto.MessageSources;

public record EmailMessageSourceDto(Guid Id, string Address)
    : MessageSourceDto(Id);