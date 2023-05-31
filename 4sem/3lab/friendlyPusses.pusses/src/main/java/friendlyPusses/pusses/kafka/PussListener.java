package friendlyPusses.pusses.kafka;

import friendlyPusses.common.dto.PussDto;
import friendlyPusses.common.kafka.messages.pusses.*;
import friendlyPusses.pusses.services.PussService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.kafka.annotation.KafkaHandler;
import org.springframework.kafka.annotation.KafkaListener;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@KafkaListener(topics = "pusses")
public class PussListener {
    private final PussService service;

    @Autowired
    public PussListener(PussService service) {
        this.service = service;
    }

    @KafkaHandler
    @SendTo
    public PussDto createPuss(CreatePussMessage request) {
        return service.create(
                request.name(),
                request.birthDate(),
                request.breed(),
                request.color(),
                request.ownerId(),
                request.user());
    }

    @KafkaHandler
    @SendTo
    public List<PussDto> getAllPusses(GetAllPussesMessage message) {
        return service.getAll(message.user());
    }

    @KafkaHandler
    @SendTo
    public PussDto getPussById(GetPussByIdMessage message) {
        return service.getById(message.pussId(), message.user());
    }

    @KafkaHandler
    @SendTo
    public List<PussDto> getPussFriends(GetPussFriendsMessage message) {
        return service.getFriends(message.pussId(), message.user());
    }

    @KafkaHandler
    @SendTo
    public List<PussDto> getFilteredPusses(GetFilteredPussesMessage message) {
        return service.getFiltered(
                message.color(),
                message.breed(),
                message.user());
    }

    @KafkaHandler
    public void changePussOwner(ChangePussOwnerMessage message) {
        service.changeOwner(
                message.pussId(),
                message.newOwnerId(),
                message.user());
    }

    @KafkaHandler
    public void makePussesFiends(MakePussesFriendsMessage message) {
        service.makeFiends(
                message.firstPussId(),
                message.secondPussId(),
                message.user());
    }

    @KafkaHandler
    public void quarrelPusses(QuarrelPussesMessage message) {
        service.quarrel(
                message.firstPussId(),
                message.secondPussId(),
                message.user());
    }

    @KafkaHandler
    public void removePuss(RemovePussMessage message) {
        service.remove(message.pussId(), message.user());
    }
}
