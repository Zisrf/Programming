package friendlyPusses.webApi.controllers;

import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("test")
public class TestController {
    @GetMapping
    public String test() {
        return "Ok";
    }

    @GetMapping("current-user")
    public UserDetails getCurrentUser(@AuthenticationPrincipal UserDetails user) {
        return user;
    }
}