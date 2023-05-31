package friendlyPusses.common.dto;

import friendlyPusses.common.entities.User;

import java.util.UUID;

public record UserDto(
        UUID id,
        String Username) {
    public static UserDto from(User user) {
        return new UserDto(
                user.getId(),
                user.getUsername());
    }
}