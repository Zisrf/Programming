package friendlyPusses.webApi.exceptionHandling;

import friendlyPusses.common.exceptions.AccessDeniedException;
import friendlyPusses.common.exceptions.NotFoundException;
import friendlyPusses.common.exceptions.ServiceException;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@ControllerAdvice
public class ExceptionHandlingController {
    @ExceptionHandler(ServiceException.class)
    public ResponseEntity<String> handlerException(ServiceException exception) {
        return ResponseEntity
                .status(500)
                .body(exception.getMessage());
    }

    @ExceptionHandler(NotFoundException.class)
    public ResponseEntity handleException(NotFoundException exception) {
        return ResponseEntity
                .status(404)
                .body(exception.getMessage());
    }

    @ExceptionHandler(AccessDeniedException.class)
    public ResponseEntity handleException(AccessDeniedException exception) {
        return ResponseEntity
                .status(403)
                .body(exception.getMessage());
    }
}