#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "archiver.h"

int getFileSize(FILE *file)
{
    fseek(file, 0, SEEK_END);
    int res = ftell(file);
    fseek(file, 0, SEEK_SET);
    return res;
}

FILE *arcOpen(struct archive *arc, char *fileName)
{
    arc->file = fopen(fileName, "rb");
    if (!arc->file)
        return NULL;
    arc->fileName = fileName;
    return arc->file;
}

void arcClose(struct archive *arc)
{
    arc->fileName = NULL;
    fclose(arc->file);
}

FILE *arcCreate(struct archive *arc, char *fileName, int filesCount, char **files)
{
    arc->file = fopen(fileName, "wb");
    if (!arc->file)
        return NULL;

    fputc(filesCount % 256, arc->file);
    fputc((filesCount >> 8) % 256, arc->file);
    fputc((filesCount >> 16) % 256, arc->file);
    fputc((filesCount >> 24) % 256, arc->file);

    FILE *file;
    int nameSize;
    int fileSize;
    for (int i = 0; i < filesCount; ++i)
    {
        file = fopen(files[i], "rb");
        if (!file)
        {
            printf("Warning: unable to open file \"%s\"\n", files[i]);
            for (int j = 0; j < 8; ++j)
                fputc(0, arc->file);
            continue;
        }

        nameSize = strlen(files[i]);
        fileSize = getFileSize(file);

        fputc(nameSize % 256, arc->file);
        fputc((nameSize >> 8) % 256, arc->file);
        fputc((nameSize >> 16) % 256, arc->file);
        fputc((nameSize >> 24) % 256, arc->file);

        fputc(fileSize % 256, arc->file);
        fputc((fileSize >> 8) % 256, arc->file);
        fputc((fileSize >> 16) % 256, arc->file);
        fputc((fileSize >> 24) % 256, arc->file);

        for (int j = 0; j < nameSize; ++j)
            fputc(files[i][j], arc->file);

        for (int j = 0; j < fileSize; ++j)
            fputc(fgetc(file), arc->file);
    }

    return arc->file;
}

void arcExtract(struct archive *arc)
{
    int filesCount = fgetc(arc->file);
    filesCount += (fgetc(arc->file) << 8);
    filesCount += (fgetc(arc->file) << 16);
    filesCount += (fgetc(arc->file) << 24);

    int nameSize, fileSize;
    char fileName[1024];
    FILE *file;
    for (int i = 0; i < filesCount; ++i)
    {
        nameSize = fgetc(arc->file);
        nameSize += (fgetc(arc->file) << 8);
        nameSize += (fgetc(arc->file) << 16);
        nameSize += (fgetc(arc->file) << 24);

        fileSize = fgetc(arc->file);
        fileSize += (fgetc(arc->file) << 8);
        fileSize += (fgetc(arc->file) << 16);
        fileSize += (fgetc(arc->file) << 24);

        if (fileSize == 0)
            continue;

        for (int j = 0; j < nameSize; ++j)
            fileName[j] = fgetc(arc->file);
        fileName[nameSize] = '\0';

        file = fopen(fileName, "wb");
        for (int j = 0; j < fileSize; ++j)
            fputc(fgetc(arc->file), file);
    }
}

void arcPrint(struct archive *arc)
{
    int filesCount = fgetc(arc->file);
    filesCount += (fgetc(arc->file) << 8);
    filesCount += (fgetc(arc->file) << 16);
    filesCount += (fgetc(arc->file) << 24);

    int nameSize, fileSize;
    for (int i = 0; i < filesCount; ++i)
    {
        nameSize = fgetc(arc->file);
        nameSize += (fgetc(arc->file) << 8);
        nameSize += (fgetc(arc->file) << 16);
        nameSize += (fgetc(arc->file) << 24);

        fileSize = fgetc(arc->file);
        fileSize += (fgetc(arc->file) << 8);
        fileSize += (fgetc(arc->file) << 16);
        fileSize += (fgetc(arc->file) << 24);

        if (fileSize == 0)
            continue;

        for (int j = 0; j < nameSize; ++j)
            printf("%c", fgetc(arc->file));
        printf("\n");

        fseek(arc->file, fileSize, SEEK_CUR);
    }
}