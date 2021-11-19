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
    char arg1[1024], fileName[1024];
    sscanf(argv[1], "%11s", arg1);
    if (strcmp(arg1, "--filepath="))
    {
        printf("Error: invalid argument \"%s\"", argv[1]);
        return 1;
    }
    strcpy(fileName, argv[1] + 11);
    if (!mp3open(mp3, fileName))
    {
        printf("Error: unable to open file \"%s\"", fileName);
        return 1;
    }

    char arg2[1024], propName[1024];
    char arg3[1024], propValue[1024];
    switch (argc)
    {
    case 3:
        sscanf(argv[2], "%6s", arg2);
        if (!strcmp(arg2, "--show"))
        {
            strcpy(propName, argv[2] + 6);
            if (!printFrames(mp3))
                printf("No information");
        }
        else if (!strcmp(arg2, "--get="))
        {
            strcpy(propName, argv[2] + 6);
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
        sscanf(argv[2], "%6s", arg2);
        sscanf(argv[3], "%8s", arg3);
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
        strcpy(propName, argv[2] + 6);
        strcpy(propValue, argv[3] + 8);
        if (!strcmp(propName, "COMM"))
        {
            memcpy(propValue + 4, propValue, strlen(propValue) + 1);
            memcpy(propValue, "rus0", 4);
        }
        setFrame(mp3, propName, propValue);
        break;
    }

    mp3close(mp3);
    free(mp3);
    return 0;
}