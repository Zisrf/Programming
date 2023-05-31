package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record GetPussFriendsMessage(
        UUID pussId,
        UserDetails user) {
}
