package friendlyPusses.common.repositories;

import friendlyPusses.common.entities.Puss;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.UUID;

public interface PussRepository extends JpaRepository<Puss, UUID> {
}
