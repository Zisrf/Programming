package friendlyPusses.controller.config;

import friendlyPusses.dao.entities.User;
import friendlyPusses.dao.models.UserRole;
import friendlyPusses.dao.repositories.UserRepository;
import friendlyPusses.service.implementations.UserServiceImpl;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

import java.util.UUID;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true)
public class SecurityConfig {
    @Bean
    public BCryptPasswordEncoder bCryptPasswordEncoder() {
        return new BCryptPasswordEncoder(12);
    }

//    @Bean
//    public UserDetailsService userDetailsService(BCryptPasswordEncoder bCryptPasswordEncoder) {
//        InMemoryUserDetailsManager manager = new InMemoryUserDetailsManager();
//        manager.createUser(User.withUsername("user")
//                .password(bCryptPasswordEncoder.encode("userPass"))
//                .authorities(UserRole.USER.getAuthorities())
//                .build());
//        manager.createUser(User.withUsername("admin")
//                .password(bCryptPasswordEncoder.encode("adminPass"))
//                .authorities(UserRole.ADMIN.getAuthorities())
//                .build());
//        return manager;
//    }

    @Bean
    public UserDetailsService userDetailsService(UserRepository userRepository, BCryptPasswordEncoder bCryptPasswordEncoder) {
        if (userRepository.findByUsername("admin") == null) {
            User user = new User(
                    UUID.randomUUID(),
                    "admin",
                    bCryptPasswordEncoder.encode("adminPass"),
                    UserRole.ADMIN);

            userRepository.save(user);
        }

        return new UserServiceImpl(userRepository, bCryptPasswordEncoder);
    }

    @Bean
    public AuthenticationManager authenticationManager(
            HttpSecurity http,
            BCryptPasswordEncoder bCryptPasswordEncoder,
            UserDetailsService userDetailsService) throws Exception {
        return http.getSharedObject(AuthenticationManagerBuilder.class)
                .userDetailsService(userDetailsService)
                .passwordEncoder(bCryptPasswordEncoder)
                .and()
                .build();
    }

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
                .csrf().disable()
                .authorizeRequests()
                .anyRequest()
                .authenticated()
                .and()
                .httpBasic();

        return http.build();
    }
}
