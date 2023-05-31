package friendlyPusses.common.models;

import lombok.Getter;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;

import java.util.Set;
import java.util.stream.Collectors;

@Getter
public enum UserRole {
    USER(Set.of(Permission.PUSSES_READ, Permission.PUSSES_WRITE, Permission.OWNERS_READ)),
    ADMIN(Set.of(Permission.PUSSES_READ, Permission.PUSSES_WRITE, Permission.OWNERS_READ, Permission.OWNERS_WRITE));

    private final Set<Permission> permissions;

    UserRole(Set<Permission> permissions) {
        this.permissions = permissions;
    }

    public Set<GrantedAuthority> getAuthorities() {
        return permissions.stream()
                .map(x -> new SimpleGrantedAuthority(x.getName()))
                .collect(Collectors.toSet());
    }
}
