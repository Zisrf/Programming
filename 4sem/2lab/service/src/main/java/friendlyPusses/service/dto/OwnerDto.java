package friendlyPusses.service.dto;

import friendlyPusses.dao.entities.Owner;

import java.util.Date;
import java.util.List;
import java.util.UUID;

public record OwnerDto(
        UUID id,
        String name,
        Date birthDate,
        List<UUID> pussesIds) {
    public static OwnerDto from(Owner owner) {
        return new OwnerDto(
                owner.getId(),
                owner.getName(),
                owner.getBirthDate(),
                owner.getPusses().stream().map(x -> x.getId()).toList());
    }
}
