using Reports.Application.Dto.MessageSources;
using Reports.Core.MessageSources;

namespace Reports.Application.Mapping;

public static class MessageSourcesMapping
{
    public static EmailMessageSourceDto AsDto(this EmailMessageSource messageSource)
    {
        return new EmailMessageSourceDto(messageSource.Id, messageSource.Email);
    }

    public static MessengerMessageSourceDto AsDto(this MessengerMessageSource messageSource)
    {
        return new MessengerMessageSourceDto(messageSource.Id, messageSource.Username);
    }

    public static SmsMessageSourceDto AsDto(this SmsMessageSource messageSource)
    {
        return new SmsMessageSourceDto(messageSource.Id, messageSource.PhoneNumber);
    }
}