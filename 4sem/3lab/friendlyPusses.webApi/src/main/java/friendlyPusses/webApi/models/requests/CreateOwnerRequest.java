package friendlyPusses.webApi.models.requests;

import lombok.Data;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Past;
import javax.validation.constraints.Size;
import java.util.Date;

@Data
public class CreateOwnerRequest {
    @NotBlank
    String name;

    @Past
    Date birthDate;

    @NotNull
    @Size(min = 5)
    String username;

    @NotNull
    @Size(min = 8)
    String password;
}