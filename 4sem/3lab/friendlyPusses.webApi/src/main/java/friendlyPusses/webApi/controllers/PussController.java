package friendlyPusses.webApi.controllers;

import friendlyPusses.common.dto.PussDto;
import friendlyPusses.common.kafka.KafkaService;
import friendlyPusses.common.kafka.messages.pusses.*;
import friendlyPusses.webApi.models.requests.CreatePussRequest;
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
@RequestMapping("api/puss")
public class PussController {
    private static final String TOPIC = "pusses";

    private final KafkaService service;

    @Autowired
    public PussController(KafkaService service) {
        this.service = service;
    }

    @PostMapping
    @PreAuthorize("hasAuthority('pusses:write')")
    public ResponseEntity<PussDto> create(@Valid @RequestBody CreatePussRequest request, @AuthenticationPrincipal UserDetails user) {
        PussDto puss = service.send(
                new CreatePussMessage(
                        request.getName(),
                        request.getBirthDate(),
                        request.getBreed(),
                        request.getColor(),
                        request.getOwnerId(),
                        user),
                TOPIC,
                PussDto.class);

        return ResponseEntity.ok(puss);
    }

    @GetMapping
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> get(@AuthenticationPrincipal UserDetails user) {
        List<PussDto> pusses = service.send(
                new GetAllPussesMessage(user),
                TOPIC,
                List.class);

        return ResponseEntity.ok(pusses);
    }

    @GetMapping("{id}")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<PussDto> getById(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        PussDto puss = service.send(
                new GetPussByIdMessage(id, user),
                TOPIC,
                PussDto.class);

        return ResponseEntity.ok(puss);
    }

    @GetMapping("{id}/friends")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> getFriends(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        List<PussDto> pusses = service.send(
                new GetPussFriendsMessage(id, user),
                TOPIC,
                List.class);

        return ResponseEntity.ok(pusses);
    }

    @GetMapping("filter")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> filter(
            @RequestParam(required = false) String color,
            @RequestParam(required = false) String breed,
            @AuthenticationPrincipal UserDetails user) {
        List<PussDto> pusses = service.send(
                new GetFilteredPussesMessage(color, breed, user),
                TOPIC,
                List.class);

        return ResponseEntity.ok(pusses);
    }

    @PutMapping("change-owner")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void changeOwner(@RequestParam UUID pussId, @RequestParam UUID newOwnerId, @AuthenticationPrincipal UserDetails user) {
        service.sendAsync(
                new ChangePussOwnerMessage(pussId, newOwnerId, user),
                TOPIC);
    }

    @PutMapping("make-friends")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void makeFriends(@RequestParam UUID firstPussId, @RequestParam UUID secondPussId, @AuthenticationPrincipal UserDetails user) {
        service.sendAsync(
                new MakePussesFriendsMessage(firstPussId, secondPussId, user),
                TOPIC);
    }

    @PutMapping("quarrel")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void quarrel(@RequestParam UUID firstPussId, @RequestParam UUID secondPussId, @AuthenticationPrincipal UserDetails user) {
        service.sendAsync(
                new QuarrelPussesMessage(firstPussId, secondPussId, user),
                TOPIC);
    }

    @DeleteMapping("{id}")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void remove(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        service.sendAsync(
                new RemovePussMessage(id, user),
                TOPIC);
    }
}
