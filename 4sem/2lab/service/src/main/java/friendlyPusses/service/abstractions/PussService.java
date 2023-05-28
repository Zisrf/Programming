package friendlyPusses.service.abstractions;

import friendlyPusses.service.dto.PussDto;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Date;
import java.util.List;
import java.util.UUID;

public interface PussService {
    PussDto create(String name, Date birthDate, String breed, String color, UUID ownerId, UserDetails user);

    List<PussDto> getAll(UserDetails user);
    PussDto getById(UUID pussId, UserDetails user);
    List<PussDto> getFriends(UUID pussId, UserDetails user);
    List<PussDto> getBy(String color, String breed, UserDetails user);

    void changeOwner(UUID pussId, UUID newOwnerId, UserDetails user);
    void makeFiends(UUID firstPussId, UUID secondPussId, UserDetails user);
    void quarrel(UUID firstPussId, UUID secondPussId, UserDetails user);

    void remove(UUID pussId, UserDetails user);
}
