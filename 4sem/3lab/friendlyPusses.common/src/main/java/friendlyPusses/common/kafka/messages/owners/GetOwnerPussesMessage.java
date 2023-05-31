package friendlyPusses.common.kafka.messages.owners;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record GetOwnerPussesMessage(
        UUID ownerId,
        UserDetails user) {
}
