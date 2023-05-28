package friendlyPusses.service.implementations;

import friendlyPusses.dao.entities.User;
import friendlyPusses.dao.models.UserRole;
import friendlyPusses.dao.repositories.OwnerRepository;
import friendlyPusses.dao.repositories.PussRepository;
import friendlyPusses.dao.entities.Owner;
import friendlyPusses.dao.entities.Puss;
import friendlyPusses.dao.models.PussBreed;
import friendlyPusses.dao.models.PussColor;
import friendlyPusses.dao.repositories.UserRepository;
import friendlyPusses.service.abstractions.PussService;
import friendlyPusses.service.abstractions.UserService;
import friendlyPusses.service.dto.PussDto;
import friendlyPusses.service.exceptions.NotFoundException;
import friendlyPusses.service.exceptions.ServiceException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Service
public class PussServiceImpl implements PussService {
    private final PussRepository pussRepository;
    private final OwnerRepository ownerRepository;
    private final UserService userService;

    @Autowired
    public PussServiceImpl(PussRepository pussRepository, OwnerRepository ownerRepository, UserService userService) {
        this.pussRepository = pussRepository;
        this.ownerRepository = ownerRepository;
        this.userService = userService;
    }

    @Override
    public PussDto create(String name, Date birthDate, String breed, String color, UUID ownerId, UserDetails user) {
        Owner owner = ownerRepository.findById(ownerId).get();
        userService.ensureHasAccess(ownerId, user);

        var puss = new Puss(
                UUID.randomUUID(),
                name, birthDate,
                PussBreed.valueOf(breed.toUpperCase()),
                PussColor.valueOf(color.toUpperCase()),
                owner);

        pussRepository.save(puss);

        return PussDto.from(puss);
    }

    @Override
    public List<PussDto> getAll(UserDetails user) {
        List<Puss> pusses = new ArrayList<>();
        pussRepository.findAll().forEach(pusses::add);

        for (Puss puss : pusses) {
            userService.ensureHasAccess(puss.getOwner().getId(), user);
        }

        return pusses.stream()
                .map(x -> PussDto.from(x))
                .toList();
    }

    @Override
    public PussDto getById(UUID pussId, UserDetails user) {
        Puss puss = getPussById(pussId);

        userService.ensureHasAccess(puss.getOwner().getId(), user);

        return PussDto.from(puss);
    }

    @Override
    public List<PussDto> getFriends(UUID pussId, UserDetails user) {
        Puss puss = getPussById(pussId);

        userService.ensureHasAccess(puss.getOwner().getId(), user);

        return puss.getFriends().stream()
                .map(x -> PussDto.from(x))
                .toList();
    }

    @Override
    public List<PussDto> getBy(String color, String breed, UserDetails user) {
        User existingUser = userService.getByUsername(user.getUsername());
        List<Puss> pusses = new ArrayList<>();
        pussRepository.findAll().forEach(pusses::add);

        return pusses.stream()
                .map(x -> PussDto.from(x))
                .filter(x -> color == null || x.color().equalsIgnoreCase(color))
                .filter(x -> breed == null || x.breed().equalsIgnoreCase(breed))
                .filter(x -> existingUser.getRole().equals(UserRole.ADMIN) || x.ownerId().equals(existingUser.getId()))
                .toList();
    }

    @Override
    public void changeOwner(UUID pussId, UUID newOwnerId, UserDetails user) {
        Puss puss = getPussById(pussId);
        userService.ensureHasAccess(puss.getOwner().getId(), user);

        Owner newOwner = getOwnerById(newOwnerId);

        puss.getOwner().getPusses().remove(puss);
        ownerRepository.save(puss.getOwner());

        newOwner.getPusses().add(puss);
        ownerRepository.save(newOwner);

        puss.setOwner(newOwner);
        pussRepository.save(puss);
    }

    @Override
    public void makeFiends(UUID firstPussId, UUID secondPussId, UserDetails user) {
        Puss firstPuss = getPussById(firstPussId);
        Puss secondPuss = getPussById(secondPussId);

        userService.ensureHasAccess(firstPuss.getOwner().getId(), user);
        userService.ensureHasAccess(secondPuss.getOwner().getId(), user);

        if (firstPuss.getFriends().contains(secondPuss) || secondPuss.getFriends().contains(secondPuss)) {
            throw new ServiceException("These pusses are already friends");
        }

        firstPuss.getFriends().add(secondPuss);
        secondPuss.getFriends().add(firstPuss);

        pussRepository.save(firstPuss);
        pussRepository.save(secondPuss);
    }

    @Override
    public void quarrel(UUID firstPussId, UUID secondPussId, UserDetails user) {
        Puss firstPuss = getPussById(firstPussId);
        Puss secondPuss = getPussById(secondPussId);

        userService.ensureHasAccess(firstPuss.getOwner().getId(), user);
        userService.ensureHasAccess(secondPuss.getOwner().getId(), user);


        if (!firstPuss.getFriends().contains(secondPuss) || !secondPuss.getFriends().contains(firstPuss)) {
            throw new ServiceException("These pusses are not friends");
        }

        firstPuss.getFriends().remove(secondPuss);
        secondPuss.getFriends().remove(firstPuss);

        pussRepository.save(firstPuss);
        pussRepository.save(secondPuss);
    }

    @Override
    public void remove(UUID pussId, UserDetails user) {
        Puss puss = getPussById(pussId);

        userService.ensureHasAccess(puss.getOwner().getId(), user);

        puss.getOwner().getPusses().remove(this);
        ownerRepository.save(puss.getOwner());

        for (Puss friend : puss.getFriends().stream().toList()) {
            quarrel(puss.getId(), friend.getId(), user);
        }

        pussRepository.delete(puss);
    }

    private Puss getPussById(UUID pussId) {
        if (!pussRepository.existsById(pussId)) {
            throw new NotFoundException("Puss with such id was not found");
        }

        return pussRepository.findById(pussId).get();
    }

    private Owner getOwnerById(UUID ownerId) {
        if (!ownerRepository.existsById(ownerId)) {
            throw new NotFoundException("Owner with such id was not found");
        }

        return ownerRepository.findById(ownerId).get();
    }
}
