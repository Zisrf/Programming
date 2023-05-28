package friendlyPusses.dao.models;

import lombok.Getter;

@Getter
public enum PussColor {
    BLACK (0, 0, 0),
    GRAY(128, 128, 128),
    BROWN(139, 69, 19),
    WHITE (255, 255, 255);

    private final int red;
    private final int green;
    private final int blue;

    PussColor(int red, int green, int blue) {
        this.red = red;
        this.green = green;
        this.blue = blue;
    }
}
