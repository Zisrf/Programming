package friendlyPusses.service.abstractions;

import friendlyPusses.dao.entities.Owner;
import friendlyPusses.dao.entities.User;
import friendlyPusses.service.dto.UserDto;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;

import java.util.UUID;

public interface UserService extends UserDetailsService {
    UserDto create(UUID id, String username, String password);

    User getByUsername(String username);

    void ensureHasAccess(UUID ownerId, UserDetails user);
}
