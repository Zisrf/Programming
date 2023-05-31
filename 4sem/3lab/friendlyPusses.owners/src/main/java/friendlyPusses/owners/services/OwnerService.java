package friendlyPusses.owners.services;

import friendlyPusses.common.dto.OwnerDto;
import friendlyPusses.common.dto.PussDto;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Date;
import java.util.List;
import java.util.UUID;

public interface OwnerService {
    OwnerDto create(String name, Date birthDate, String username, String password);

    List<OwnerDto> getAll(UserDetails user);
    OwnerDto getById(UUID ownerId, UserDetails user);
    List<PussDto> getPusses(UUID ownerId, UserDetails user);

    void remove(UUID ownerId, UserDetails user);
}