package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record ChangePussOwnerMessage(
        UUID pussId,
        UUID newOwnerId,
        UserDetails user) {
}
