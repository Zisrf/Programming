package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record RemovePussMessage(
        UUID pussId,
        UserDetails user) {
}
