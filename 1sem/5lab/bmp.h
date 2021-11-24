#include <stdio.h>

struct bmp
{
    FILE *file;
    int size;
    int offsetBits;
    int h, w;
    int bitCount;
};

FILE *bmpCopy(struct bmp *, char *, struct bmp *);

FILE *bmpOpen(struct bmp *, char *);

void bmpClose(struct bmp *);

void bmpFromArr(struct bmp *, char **);

void bmpToArr(struct bmp *, char **);