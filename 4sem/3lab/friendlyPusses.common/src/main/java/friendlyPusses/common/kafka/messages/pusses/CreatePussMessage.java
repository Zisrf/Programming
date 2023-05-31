package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

import java.util.Date;
import java.util.UUID;

public record CreatePussMessage(
        String name,
        Date birthDate,
        String breed,
        String color,
        UUID ownerId,
        UserDetails user) {
}
