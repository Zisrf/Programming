package friendlyPusses.service.implementations;

import friendlyPusses.dao.entities.User;
import friendlyPusses.dao.models.UserRole;
import friendlyPusses.dao.repositories.UserRepository;
import friendlyPusses.service.abstractions.UserService;
import friendlyPusses.service.dto.UserDto;
import friendlyPusses.service.exceptions.AccessDeniedException;
import friendlyPusses.service.exceptions.NotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class UserServiceImpl implements UserService {
    private final UserRepository userRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;

    @Autowired
    public UserServiceImpl(UserRepository userRepository, BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.userRepository = userRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRepository.findByUsername(username);

        if (user == null) {
            throw new UsernameNotFoundException("User not found");
        }

        return new org.springframework.security.core.userdetails.User(
                user.getUsername(),
                user.getPassword(),
                user.getRole().getAuthorities()
        );
    }

    @Override
    public UserDto create(UUID id, String username, String password) {
        if (userRepository.findByUsername(username) != null) {
            throw new RuntimeException("User with such username already exists");
        }

        User user = new User(id, username, bCryptPasswordEncoder.encode(password), UserRole.USER);

        userRepository.save(user);

        return UserDto.from(user);
    }

    @Override
    public User getByUsername(String username) {
        User existingUser = userRepository.findByUsername(username);

        if (existingUser == null) {
            throw new NotFoundException("User with such username was not found");
        }

        return existingUser;
    }

    @Override
    public void ensureHasAccess(UUID ownerId, UserDetails user) {
        User existingUser = getByUsername(user.getUsername());

        if (existingUser.getRole().equals(UserRole.ADMIN)) {
            return;
        }

        if (existingUser.getId().equals(ownerId)) {
            return;
        }

        throw new AccessDeniedException("Access to such owner resources denied");
    }
}
