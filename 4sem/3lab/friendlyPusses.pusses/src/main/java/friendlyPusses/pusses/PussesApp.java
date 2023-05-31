package friendlyPusses.pusses;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@SpringBootApplication
@EnableJpaRepositories("friendlyPusses.common.repositories")
@EntityScan("friendlyPusses.common.entities")
@ComponentScan("friendlyPusses.*")
public class PussesApp {
    public static void main(String[] args) {
        SpringApplication.run(PussesApp.class, args);
    }
}