package friendlyPusses.owners.kafka;

import friendlyPusses.common.dto.OwnerDto;
import friendlyPusses.common.dto.PussDto;
import friendlyPusses.common.kafka.messages.owners.*;
import friendlyPusses.owners.services.OwnerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.annotation.KafkaHandler;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@KafkaListener(topics = "owners")
public class OwnerListener {
    private final OwnerService service;

    @Autowired
    public OwnerListener(OwnerService service) {
        this.service = service;
    }

    @KafkaHandler
    @SendTo
    public OwnerDto createOwner(CreateOwnerMessage message) {
        return service.create(
                message.name(),
                message.birthDate(),
                message.username(),
                message.password());
    }

    @KafkaHandler
    @SendTo
    public List<OwnerDto> getAllOwners(GetAllOwnersMessage message) {
        return service.getAll(message.user());
    }

    @KafkaHandler
    @SendTo
    public OwnerDto getOwnerById(GetOwnerByIdMessage request) {
        return service.getById(request.ownerId(), request.user());
    }

    @KafkaHandler
    @SendTo
    public List<PussDto> getOwnerPusses(GetOwnerPussesMessage message) {
        return service.getPusses(message.ownerId(), message.user());
    }

    @KafkaHandler
    public void removeOwner(RemoveOwnerMessage message) {
        service.remove(message.ownerId(), message.user());
    }
}
