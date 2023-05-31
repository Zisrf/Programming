package friendlyPusses.common.kafka.messages.users;

import java.util.UUID;

public record CreateUserMessage(
        UUID id,
        String username,
        String password) {
}
