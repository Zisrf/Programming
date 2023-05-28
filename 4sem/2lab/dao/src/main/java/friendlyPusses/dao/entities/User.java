package friendlyPusses.dao.entities;

import friendlyPusses.dao.models.UserRole;
import jakarta.persistence.*;
import lombok.Data;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import java.util.Collection;
import java.util.Set;
import java.util.UUID;

@Data
@Entity
public class User {
    @Id
    private UUID id;

    private String username;

    private String password;

    @Enumerated(value = EnumType.STRING)
    private UserRole role;

    public User(UUID id, String username, String password, UserRole role) {
        this.id = id;
        this.username = username;
        this.password = password;
        this.role = role;
    }

    protected User() { }
}
