package friendlyPusses.webApi.models.requests;

import lombok.Data;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Past;
import java.util.Date;
import java.util.UUID;

@Data
public class CreatePussRequest {
    @NotBlank
    String name;

    @Past
    Date birthDate;

    @NotBlank
    String breed;

    @NotBlank
    String color;

    @NotNull
    UUID ownerId;
}