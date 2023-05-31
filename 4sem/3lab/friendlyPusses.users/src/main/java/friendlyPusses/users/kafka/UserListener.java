package friendlyPusses.users.kafka;

import friendlyPusses.common.dto.UserDto;
import friendlyPusses.common.entities.User;
import friendlyPusses.common.kafka.messages.users.CheckUserAccessMessage;
import friendlyPusses.common.kafka.messages.users.CreateUserMessage;
import friendlyPusses.common.kafka.messages.users.GetUserByUsernameMessage;
import friendlyPusses.users.services.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.annotation.KafkaHandler;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Service;

@Service
@KafkaListener(topics = "users")
public class UserListener {
    private final UserService service;

    @Autowired
    public UserListener(UserService service) {
        this.service = service;
    }

    @KafkaHandler
    @SendTo
    public UserDto createUser(CreateUserMessage message) {
        return service.create(
                message.id(),
                message.username(),
                message.password());
    }

    @KafkaHandler
    @SendTo
    public User getUserByUsername(GetUserByUsernameMessage message) {
        return service.getByUsername(message.username());
    }

    @KafkaHandler
    @SendTo
    boolean checkUserAccess(CheckUserAccessMessage message) {
        return service.checkAccess(message.ownerId(), message.user());
    }
}
