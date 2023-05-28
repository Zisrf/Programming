package friendlyPusses.service.implementations;

import friendlyPusses.dao.entities.Owner;
import friendlyPusses.dao.entities.Puss;
import friendlyPusses.dao.repositories.OwnerRepository;
import friendlyPusses.service.abstractions.OwnerService;
import friendlyPusses.service.abstractions.PussService;
import friendlyPusses.service.abstractions.UserService;
import friendlyPusses.service.dto.OwnerDto;
import friendlyPusses.service.dto.PussDto;
import friendlyPusses.service.exceptions.NotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Service
public class OwnerServiceImpl implements OwnerService {
    private final OwnerRepository ownerRepository;
    private final PussService pussService;
    private final UserService userService;

    @Autowired
    public OwnerServiceImpl(OwnerRepository ownerRepository, PussService pussService, UserService userService) {
        this.ownerRepository = ownerRepository;
        this.pussService = pussService;
        this.userService = userService;
    }

    @Override
    public OwnerDto create(String name, Date birthDate, String username, String password) {
        var owner = new Owner(UUID.randomUUID(), name, birthDate);

        userService.create(owner.getId(), username, password);
        ownerRepository.save(owner);

        return OwnerDto.from(owner);
    }

    @Override
    public List<OwnerDto> getAll(UserDetails user) {
        List<Owner> owners = new ArrayList<>();
        ownerRepository.findAll().forEach(owners::add);

        for (Owner owner : owners) {
            userService.ensureHasAccess(owner.getId(), user);
        }

        return owners.stream()
                .map(x -> OwnerDto.from(x))
                .toList();
    }

    @Override
    public OwnerDto getById(UUID ownerId, UserDetails user) {
        userService.ensureHasAccess(ownerId, user);

        Owner owner = getOwnerById(ownerId);

        return OwnerDto.from(owner);
    }

    @Override
    public List<PussDto> getPusses(UUID ownerId, UserDetails user) {
        userService.ensureHasAccess(ownerId, user);

        Owner owner = getOwnerById(ownerId);

        return owner.getPusses().stream()
                .map(x -> PussDto.from(x))
                .toList();
    }

    @Override
    public void remove(UUID ownerId, UserDetails user) {
        Owner owner = getOwnerById(ownerId);

        for (Puss puss : owner.getPusses().stream().toList()) {
            pussService.remove(puss.getId(), user);
        }

        ownerRepository.delete(owner);
    }

    private Owner getOwnerById(UUID ownerId) {
        if (!ownerRepository.existsById(ownerId)) {
            throw new NotFoundException("Owner with such id was not found");
        }

        return ownerRepository.findById(ownerId).get();
    }
}
