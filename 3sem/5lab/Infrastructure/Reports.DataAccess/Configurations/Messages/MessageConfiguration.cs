using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Messages;

namespace Reports.DataAccess.Configurations.Messages;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasDiscriminator()
            .HasValue<EmailMessage>(nameof(EmailMessage))
            .HasValue<MessengerMessage>(nameof(MessengerMessage))
            .HasValue<SmsMessage>(nameof(SmsMessage));
    }
}
