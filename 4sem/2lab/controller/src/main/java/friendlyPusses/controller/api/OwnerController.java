package friendlyPusses.controller.api;

import friendlyPusses.controller.models.CreateOwnerRequest;
import friendlyPusses.service.abstractions.OwnerService;
import friendlyPusses.service.dto.OwnerDto;
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
@RequestMapping("api/owner")
public class OwnerController {
    private final OwnerService service;

    @Autowired
    public OwnerController(OwnerService service) {
        this.service = service;
    }

    @PostMapping
    @PreAuthorize("hasAuthority('owners:write')")
    public ResponseEntity<OwnerDto> create(@Valid @RequestBody CreateOwnerRequest request) {
        return ResponseEntity.ok(service.create(
                request.getName(),
                request.getBirthDate(),
                request.getUsername(),
                request.getPassword()));
    }

    @GetMapping
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<List<OwnerDto>> get(@AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getAll(user));
    }

    @GetMapping("{id}")
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<OwnerDto> getById(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getById(id, user));
    }

    @GetMapping("{id}/pusses")
    @PreAuthorize("hasAuthority('owners:read')")
    public ResponseEntity<List<PussDto>> getPusses(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        return ResponseEntity.ok(service.getPusses(id, user));
    }

    @DeleteMapping("{id}")
    @PreAuthorize("hasAuthority('owners:write')")
    public void remove(@PathVariable("id") UUID id, @AuthenticationPrincipal UserDetails user) {
        service.remove(id, user);
    }
}