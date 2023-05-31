package friendlyPusses.common.entities;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.OneToMany;
import lombok.Data;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Data
@Entity
public class Owner {
    @Id
    private UUID id;

    private String name;

    private Date birthDate;

    @OneToMany(mappedBy = "owner")
    private List<Puss> pusses;

    public Owner(UUID id, String name, Date birthDate) {
        this.id = id;
        this.name = name;
        this.birthDate = birthDate;

        this.pusses = new ArrayList<>();
    }

    protected Owner () { }
}
