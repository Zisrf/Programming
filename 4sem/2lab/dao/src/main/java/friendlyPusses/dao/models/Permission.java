package friendlyPusses.dao.models;

import lombok.Getter;

@Getter
public enum Permission {
    PUSSES_READ("pusses:read"),
    PUSSES_WRITE("pusses:write"),
    OWNERS_READ("owners:read"),
    OWNERS_WRITE("owners:write");

    private final String name;

    Permission(String name) {
        this.name = name;
    }
}
