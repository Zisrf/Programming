package friendlyPusses.controller.api;

import friendlyPusses.controller.models.CreatePussRequest;
import friendlyPusses.service.abstractions.PussService;
import friendlyPusses.service.dto.PussDto;
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
    private final PussService service;

    @Autowired
    public PussController(PussService service) {
        this.service = service;
    }

    @PostMapping
    @PreAuthorize("hasAuthority('pusses:write')")
    public ResponseEntity<PussDto> create(@Valid @RequestBody CreatePussRequest request, @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.create(
                request.getName(),
                request.getBirthDate(),
                request.getBreed(),
                request.getColor(),
                request.getOwnerId(),
                user));
    }

    @GetMapping
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> get(@AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getAll(user));
    }

    @GetMapping("{id}")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<PussDto> getById(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getById(id, user));
    }

    @GetMapping("{id}/friends")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> getFriends(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getFriends(id, user));
    }

    @GetMapping("filter")
    @PreAuthorize("hasAuthority('pusses:read')")
    public ResponseEntity<List<PussDto>> filter(
            @RequestParam(required = false) String color,
            @RequestParam(required = false) String breed,
            @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getBy(color, breed, user));
    }

    @PutMapping("change-owner")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void changeOwner(@RequestParam UUID pussId, @RequestParam UUID newOwnerId, @AuthenticationPrincipal UserDetails user) {
        service.changeOwner(pussId, newOwnerId, user);
    }

    @PutMapping("make-friends")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void makeFriends(@RequestParam UUID firstPussId, @RequestParam UUID secondPussId, @AuthenticationPrincipal UserDetails user) {
        service.makeFiends(firstPussId, secondPussId, user);
    }

    @PutMapping("quarrel")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void quarrel(@RequestParam UUID firstPussId, @RequestParam UUID secondPussId, @AuthenticationPrincipal UserDetails user) {
        service.quarrel(firstPussId, secondPussId, user);
    }

    @DeleteMapping("{id}")
    @PreAuthorize("hasAuthority('pusses:write')")
    public void remove(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        service.remove(id, user);
    }
}