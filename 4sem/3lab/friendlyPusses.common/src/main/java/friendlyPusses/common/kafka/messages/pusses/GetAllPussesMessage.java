package friendlyPusses.common.kafka.messages.pusses;

import org.springframework.security.core.userdetails.UserDetails;

public record GetAllPussesMessage(
        UserDetails user) {
}
