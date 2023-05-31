package friendlyPusses.owners.services;

import friendlyPusses.common.dto.OwnerDto;
import friendlyPusses.common.dto.PussDto;
import friendlyPusses.common.entities.Owner;
import friendlyPusses.common.entities.Puss;
import friendlyPusses.common.exceptions.AccessDeniedException;
import friendlyPusses.common.exceptions.NotFoundException;
import friendlyPusses.common.kafka.KafkaService;
import friendlyPusses.common.kafka.messages.pusses.RemovePussMessage;
import friendlyPusses.common.kafka.messages.users.CheckUserAccessMessage;
import friendlyPusses.common.kafka.messages.users.CreateUserMessage;
import friendlyPusses.common.repositories.OwnerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Service
public class OwnerServiceImpl implements OwnerService {
    private final OwnerRepository ownerRepository;
    private final KafkaService kafkaService;

    @Autowired
    public OwnerServiceImpl(OwnerRepository ownerRepository, KafkaService kafkaService) {
        this.ownerRepository = ownerRepository;
        this.kafkaService = kafkaService;
    }

    @Override
    public OwnerDto create(String name, Date birthDate, String username, String password) {
        var owner = new Owner(UUID.randomUUID(), name, birthDate);

        kafkaService.sendAsync(
                new CreateUserMessage(owner.getId(), username, password),
                "users");

        ownerRepository.save(owner);

        return OwnerDto.from(owner);
    }

    @Override
    public List<OwnerDto> getAll(UserDetails user) {
        List<Owner> owners = new ArrayList<>();
        ownerRepository.findAll().forEach(owners::add);

        for (Owner owner : owners) {
            ensureHasAccess(owner.getId(), user);
        }

        return owners.stream()
                .map(x -> OwnerDto.from(x))
                .toList();
    }

    @Override
    public OwnerDto getById(UUID ownerId, UserDetails user) {
        ensureHasAccess(ownerId, user);

        Owner owner = getOwnerById(ownerId);

        return OwnerDto.from(owner);
    }

    @Override
    public List<PussDto> getPusses(UUID ownerId, UserDetails user) {
        ensureHasAccess(ownerId, user);

        Owner owner = getOwnerById(ownerId);

        return owner.getPusses().stream()
                .map(x -> PussDto.from(x))
                .toList();
    }

    @Override
    public void remove(UUID ownerId, UserDetails user) {
        Owner owner = getOwnerById(ownerId);

        for (Puss puss : owner.getPusses().stream().toList()) {
            kafkaService.sendAsync(
                    new RemovePussMessage(puss.getId(), user),
                    "pusses");
        }

        ownerRepository.delete(owner);
    }

    private Owner getOwnerById(UUID ownerId) {
        if (!ownerRepository.existsById(ownerId)) {
            throw new NotFoundException("Owner with such id was not found");
        }

        return ownerRepository.findById(ownerId).get();
    }

    private void ensureHasAccess(UUID ownerId, UserDetails user) {
        boolean hasAccess = kafkaService.send(
                new CheckUserAccessMessage(ownerId, user),
                "users",
                boolean.class);

        if (!hasAccess) {
            throw new AccessDeniedException("Access to such owner resources denied");
        }
    }
}