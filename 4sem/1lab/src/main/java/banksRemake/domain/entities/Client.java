package banksRemake.domain.entities;

import lombok.Getter;
import lombok.Setter;

import java.util.UUID;

/**
 * Bank client.
 */
public class Client {
    @Getter
    private final UUID id;
    @Getter
    private final String name;
    @Getter
    @Setter
    private String address;
    @Getter
    @Setter
    private String passport;

    public Client(String name)
    {
        this.name = name;

        this.id = UUID.randomUUID();
    }

    /**
     * Checking that the user's passport and address are specified.
     */
    public boolean isVerified() {
        return address != null && passport != null;
    }
}
