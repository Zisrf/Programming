package friendlyPusses.users.services;

import friendlyPusses.common.dto.UserDto;
import friendlyPusses.common.entities.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;

import java.util.UUID;

public interface UserService extends UserDetailsService {
    UserDto create(UUID id, String username, String password);

    User getByUsername(String username);

    boolean checkAccess(UUID ownerId, UserDetails user);
}