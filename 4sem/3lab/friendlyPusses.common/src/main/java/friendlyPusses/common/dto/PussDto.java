package friendlyPusses.common.dto;

import friendlyPusses.common.entities.Puss;

import java.util.Date;
import java.util.List;
import java.util.UUID;

public record PussDto(
        UUID id,
        String name,
        Date birthDate,
        String breed,
        String color,
        UUID ownerId,
        List<UUID> friendsIds) {
    public static PussDto from(Puss puss) {
        return new PussDto(
                puss.getId(),
                puss.getName(),
                puss.getBirthDate(),
                puss.getBreed().toString(),
                puss.getColor().toString(),
                puss.getOwner().getId(),
                puss.getFriends().stream().map(x -> x.getId()).toList());
    }
}