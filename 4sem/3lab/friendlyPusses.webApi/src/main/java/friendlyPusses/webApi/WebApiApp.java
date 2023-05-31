package friendlyPusses.webApi;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.kafka.annotation.EnableKafka;

@EnableKafka
@EnableJpaRepositories("friendlyPusses.common.repositories")
@EntityScan("friendlyPusses.common.entities")
@ComponentScan("friendlyPusses.*")
@SpringBootApplication
public class WebApiApp {
    public static void main(String[] args) {
        SpringApplication.run(WebApiApp.class, args);
    }
}
