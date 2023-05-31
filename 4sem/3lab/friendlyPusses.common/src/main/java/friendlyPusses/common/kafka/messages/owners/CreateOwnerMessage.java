package friendlyPusses.common.kafka.messages.owners;

import java.util.Date;

public record CreateOwnerMessage(
        String name,
        Date birthDate,
        String username,
        String password) {
}
