package friendlyPusses.webApi.controllers;

import friendlyPusses.common.dto.OwnerDto;
import friendlyPusses.common.dto.PussDto;
import friendlyPusses.common.kafka.KafkaService;
import friendlyPusses.common.kafka.messages.owners.*;
import friendlyPusses.webApi.models.requests.CreateOwnerRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("api/owner")
public class OwnerController {
    private static final String TOPIC = "owners";

    private final KafkaService service;

    @Autowired
    public OwnerController(KafkaService service) {
        this.service = service;
    }

    @PostMapping
    @PreAuthorize("hasAuthority('owners:write')")
    public ResponseEntity<OwnerDto> create(@Valid @RequestBody CreateOwnerRequest request) {
        OwnerDto owner = service.send(
                new CreateOwnerMessage(
                        request.getName(),
                        request.getBirthDate(),
                        request.getUsername(),
                        request.getPassword()),
                TOPIC,
                OwnerDto.class);

        return ResponseEntity.ok(owner);
    }

    @GetMapping
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<List<OwnerDto>> get(@AuthenticationPrincipal UserDetails user) {
        List<OwnerDto> owners = service.send(
                new GetAllOwnersMessage(user),
                TOPIC,
                List.class);

        return ResponseEntity.ok(owners);
    }

    @GetMapping("{id}")
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<OwnerDto> getById(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        OwnerDto owner = service.send(
                new GetOwnerByIdMessage(id, user),
                TOPIC,
                OwnerDto.class);

        return ResponseEntity.ok(owner);
    }

    @GetMapping("{id}/pusses")
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<List<PussDto>> getPusses(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        List<PussDto> pusses = service.send(
                new GetOwnerPussesMessage(id, user),
                TOPIC,
                List.class);

        return ResponseEntity.ok(pusses);
    }

    @DeleteMapping("{id}")
    @PreAuthorize("hasAuthority('owners:write')")
    public void remove(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        service.sendAsync(
                new RemoveOwnerMessage(id, user),
                TOPIC);
    }
}
