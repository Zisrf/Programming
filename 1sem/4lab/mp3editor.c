#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "mp3file.h"

int main(int argc, char *argv[])
{
    if (argc < 3 || argc > 4)
    {
        printf("Error: invalid number of arguments");
        return 1;
    }

    struct mp3file *mp3 = calloc(1, sizeof(struct mp3file));
    char arg1[128], fileName[128];
    sscanf(argv[1], "%11s%s", arg1, fileName);
    if (strcmp(arg1, "--filepath="))
    {
        printf("Error: invalid argument \"%s\"", argv[1]);
        return 1;
    }
    if (!mp3open(mp3, fileName))
    {
        printf("Error: unable to open file \"%s\"", fileName);
        return 1;
    }

    char arg2[128], propName[128];
    char arg3[128], propValue[128];
    switch (argc)
    {
    case 3:
        sscanf(argv[2], "%6s%s", arg2, propName);
        if (!strcmp(arg2, "--show"))
        {
            if (!printFrames(mp3))
                printf("No information");
        }
        else if (!strcmp(arg2, "--get="))
        {
            if (!getFrame(mp3, propName))
                printf("No such frame");
        }
        else
        {
            printf("Error: invalid argument \"%s\"", argv[2]);
            return 1;
        }
        break;
    case 4:
        sscanf(argv[2], "%6s%s", arg2, propName);
        sscanf(argv[3], "%8s%s", arg3, propValue);
        if (strcmp(arg2, "--set="))
        {
            printf("Error: invalid argument \"%s\"", argv[2]);
            return 1;
        }
        if (strcmp(arg3, "--value="))
        {
            printf("Error: invalid argument \"%s\"", argv[3]);
            return 1;
        }
        setFrame(mp3, propName, propValue);
        break;
    }

    mp3close(mp3);
    free(mp3);
    return 0;
}