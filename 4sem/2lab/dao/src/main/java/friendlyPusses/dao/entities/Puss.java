package friendlyPusses.dao.entities;

import friendlyPusses.dao.models.PussBreed;
import friendlyPusses.dao.models.PussColor;
import jakarta.persistence.*;
import lombok.Data;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

@Data
@Entity
public class Puss {
    @Id
    private UUID id;

    private String name;

    private Date birthDate;

    private PussBreed breed;

    private PussColor color;

    @ManyToOne
    @JoinColumn(name = "owner_id")
    private Owner owner;

    @ManyToMany
    private List<Puss> friends;

    public Puss(UUID id, String name, Date birthDate, PussBreed breed, PussColor color, Owner owner) {
        this.id = id;
        this.name = name;
        this.birthDate = birthDate;
        this.breed = breed;
        this.color = color;
        this.owner = owner;

        this.friends = new ArrayList<>();
    }

    protected Puss() { }
}
