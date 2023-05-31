package friendlyPusses.common.kafka.messages.owners;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.UUID;

public record GetOwnerByIdMessage(
        UUID ownerId,
        UserDetails user) {
}
