package friendlyPusses.common.kafka.messages.users;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record CheckUserAccessMessage(
        UUID ownerId,
        UserDetails user) {
}
