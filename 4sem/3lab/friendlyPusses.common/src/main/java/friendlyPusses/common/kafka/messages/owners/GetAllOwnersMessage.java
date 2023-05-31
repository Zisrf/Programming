package friendlyPusses.common.kafka.messages.owners;

import org.springframework.security.core.userdetails.UserDetails;

public record GetAllOwnersMessage(
        UserDetails user) {
}
