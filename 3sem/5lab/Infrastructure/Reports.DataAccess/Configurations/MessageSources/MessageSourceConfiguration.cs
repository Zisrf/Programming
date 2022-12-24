using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.MessageSources;

namespace Reports.DataAccess.Configurations.MessageSources;

public class MessageSourceConfiguration : IEntityTypeConfiguration<MessageSource>
{
    public void Configure(EntityTypeBuilder<MessageSource> builder)
    {
        builder.HasDiscriminator()
            .HasValue<EmailMessageSource>(nameof(EmailMessageSource))
            .HasValue<MessengerMessageSource>(nameof(MessengerMessageSource))
            .HasValue<SmsMessageSource>(nameof(SmsMessageSource));

        builder.Ignore(x => x.Messages);
    }
}