#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "archiver.h"

int main(int argc, char *argv[])
{
    struct archive *arc = calloc(1, sizeof(struct archive));
    char *arcName;
    int filesCount;
    char **files;

    if (argc < 4)
    {
        printf("Error: invalid number of arguments");
        return 1;
    }

    if (strcmp(argv[1], "--file"))
    {
        printf("Error: invalid argument \"%s\"", argv[1]);
        return 1;
    }
    arcName = argv[2];

    if (!strcmp(argv[3], "--create"))
    {
        filesCount = argc - 4;
        files = argv + 4;
        if (!arcCreate(arc, arcName, filesCount, files))
        {
            printf("Error: unable to open file \"%s\"", arcName);
            return 1;
        }
    }
    else if (!strcmp(argv[3], "--extract"))
    {
        if (!arcOpen(arc, arcName))
        {
            printf("Error: unable to open file \"%s\"", arcName);
            return 1;
        }
        arcExtract(arc);
    }
    else if (!strcmp(argv[3], "--list"))
    {
        if (!arcOpen(arc, arcName))
        {
            printf("Error: unable to open file \"%s\"", arcName);
            return 1;
        }
        arcPrint(arc);
    }
    else
    {
        printf("Error: invalid argument \"%s\"", argv[3]);
        return 1;
    }

    arcClose(arc);
    free(arc);
    return 0;
}