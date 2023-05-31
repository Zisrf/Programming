package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

public record GetFilteredPussesMessage(
        String color,
        String breed,
        UserDetails user) {
}
