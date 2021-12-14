#include <stdio.h>

struct archive
{
    FILE *file;
};

FILE *arcOpen(struct archive *, char *);

void arcClose(struct archive *);

FILE *arcCreate(struct archive *, char *, int, char **);

void arcExtract(struct archive *);

void arcPrint(struct archive *);