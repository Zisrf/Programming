#include <stdio.h>
#include "bmp.h"

FILE *bmpCopy(struct bmp *pic, char *fileName, struct bmp *src)
{
    pic->file = fopen(fileName, "wb");
    if (!pic->file)
        return NULL;

    fseek(src->file, 0, SEEK_SET);
    for (int i = 0; i < src->size; ++i)
        fputc(fgetc(src->file), pic->file);

    pic->size = src->size;
    pic->offsetBits = src->offsetBits;
    pic->h = src->h;
    pic->w = src->w;
    pic->bitCount = src->bitCount;

    return pic->file;
}

FILE *bmpOpen(struct bmp *pic, char *fileName)
{
    pic->file = fopen(fileName, "rb+");
    if (!pic->file)
        return NULL;

    char c1 = fgetc(pic->file);
    char c2 = fgetc(pic->file);
    if (c1 != 'B' || c2 != 'M')
        return NULL;

    pic->size = fgetc(pic->file);
    pic->size += (fgetc(pic->file) << 8);
    pic->size += (fgetc(pic->file) << 16);
    pic->size += (fgetc(pic->file) << 24);

    fseek(pic->file, 10, SEEK_SET);
    pic->offsetBits = fgetc(pic->file);
    pic->offsetBits += (fgetc(pic->file) << 8);
    pic->offsetBits += (fgetc(pic->file) << 16);
    pic->offsetBits += (fgetc(pic->file) << 24);

    fseek(pic->file, 18, SEEK_SET);
    pic->w = fgetc(pic->file);
    pic->w += (fgetc(pic->file) << 8);
    pic->w += (fgetc(pic->file) << 16);
    pic->w += (fgetc(pic->file) << 24);

    pic->h = fgetc(pic->file);
    pic->h += (fgetc(pic->file) << 8);
    pic->h += (fgetc(pic->file) << 16);
    pic->h += (fgetc(pic->file) << 24);

    fseek(pic->file, 28, SEEK_SET);
    pic->bitCount = fgetc(pic->file);
    pic->bitCount += (fgetc(pic->file) << 8);
    if (pic->bitCount > 1)
        return NULL;

    return pic->file;
}

void bmpClose(struct bmp *pic)
{
    if (pic == NULL)
        return;
    fclose(pic->file);
    pic->file = NULL;
    pic->size = 0;
    pic->offsetBits = 0;
    pic->h = 0;
    pic->w = 0;
    pic->bitCount = 0;
}

void bmpFromArr(struct bmp *pic, char **cell)
{
    fseek(pic->file, pic->offsetBits, SEEK_SET);
    int rowSize = (pic->w + 31) / 32 * 4;
    for (int i = pic->h - 1; i >= 0; --i)
        for (int j = 0; j < rowSize; ++j)
        {
            int byte = 0;
            for (int k = 0; k < 8; ++k)
                if (8 * j + k < pic->w)
                    byte += ((!cell[i][8 * j + k]) << (7 - k));
            fputc(byte, pic->file);
        }
}

void bmpToArr(struct bmp *pic, char **cell)
{
    fseek(pic->file, pic->offsetBits, SEEK_SET);
    int rowSize = (pic->w + 31) / 32 * 4;
    for (int i = pic->h - 1; i >= 0; --i)
        for (int j = 0; j < rowSize; ++j)
        {
            int byte = fgetc(pic->file);
            for (int k = 0; k < 8; ++k)
                if (8 * j + k < pic->w)
                    cell[i][8 * j + k] = !((byte >> (7 - k)) & 1);
        }
}