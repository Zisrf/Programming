#include <stdio.h>

struct mp3file
{
    FILE *file;
    char *name;
    char *str;
    int size;
};

FILE *mp3open(struct mp3file *, char *);

void mp3update(struct mp3file *);

void mp3close(struct mp3file *);

int isFrameHeader(char *);

int findFrame(struct mp3file *, char *);

int printFrames(struct mp3file *);

int getFrame(struct mp3file *, char *);

void setFrame(struct mp3file *, char *, char *);