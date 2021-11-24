#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <windows.h>
#include "bmp.h"
#include "set.h"

const int INF = 2e9 + 11;

int dh[] = {0, 1, 0, -1, 1, 1, -1, -1};
int dw[] = {1, 0, -1, 0, 1, -1, 1, -1};

void nextGeneration(char **cell, int h, int w)
{
    char **neighborsCount = malloc(h * sizeof(char *));
    for (int i = 0; i < h; ++i)
        neighborsCount[i] = calloc(w, sizeof(char));

    for (int i = 0; i < h; ++i)
        for (int j = 0; j < w; ++j)
            for (int k = 0; k < 8; ++k)
                neighborsCount[i][j] += cell[(h + i + dh[k]) % h][(w + j + dw[k]) % w];

    for (int i = 0; i < h; ++i)
        for (int j = 0; j < w; ++j)
        {
            if (!cell[i][j] && neighborsCount[i][j] == 3)
                cell[i][j] = 1;
            if (cell[i][j] && neighborsCount[i][j] < 2)
                cell[i][j] = 0;
            if (cell[i][j] && neighborsCount[i][j] > 3)
                cell[i][j] = 0;
        }

    for (int i = 0; i < h; ++i)
        free(neighborsCount[i]);
    free(neighborsCount);
}

void generate(char *inputFile, char *dirName, int maxIter, double dumpFreq)
{
    struct bmp *input = calloc(1, sizeof(struct bmp));
    struct bmp *pic = calloc(1, sizeof(struct bmp));
    struct set *s = malloc(sizeof(struct set));
    char fileName[1024];
    char **cell;

    if (!bmpOpen(input, inputFile))
    {
        printf("Error: unable to open file \"%s\"", inputFile);
        exit(1);
    }
    int h = input->h;
    int w = input->w;
    cell = malloc(h * sizeof(char *));
    for (int i = 0; i < h; ++i)
        cell[i] = malloc(w);
    bmpToArr(input, cell);
    init(s);

    sprintf(fileName, "%s\\%d.bmp", dirName, 0);
    if (!bmpCopy(pic, fileName, input))
    {
        char cmd[1024];
        sprintf(cmd, "md %s", dirName);
        system(cmd);
        bmpCopy(pic, fileName, input);
    }
    bmpFromArr(pic, cell);
    bmpClose(pic);
    insert(s, cell, h, w);
    for (int i = 1; i <= maxIter; ++i)
    {
        nextGeneration(cell, h, w);
        if (exists(s, cell, h, w))
            break;
        insert(s, cell, h, w);

        sprintf(fileName, "%s\\%d.bmp", dirName, i);
        bmpCopy(pic, fileName, input);
        bmpFromArr(pic, cell);
        bmpClose(pic);

        if (dumpFreq > 0 && 1000 / dumpFreq * i - clock() > 0)
            Sleep((int)(1000 / dumpFreq * i - clock()));
    }

    for (int i = 0; i < h; ++i)
        free(cell[i]);
    free(cell);
    bmpClose(input);
    free(input);
    free(pic);
    del(s);
    free(s);
}

int isCorrectInt(char *str)
{
    for (int i = 0; str[i] != '\0'; ++i)
        if (!(str[i] >= '0' && str[i] <= '9'))
            return 0;
    return 1;
}

int isCorrectDouble(char *str)
{
    if (!(str[0] >= '0' && str[0] <= '9'))
        return 0;
    for (int i = 1; str[i] != '\0'; ++i)
        if (!(str[i] >= '0' && str[i] <= '9' || str[i] == '.'))
            return 0;
    return 1;
}

int main(int argc, char *argv[])
{
    char inputFile[1024] = "";
    char dirName[1024] = "";
    int maxIter = INF;
    double dumpFreq = 1;

    if (argc % 2 == 0)
    {
        printf("Error: invalid number of arguments");
        return 1;
    }

    for (int i = 1; i < argc; ++i)
    {
        if (!strcmp(argv[i], "--input"))
        {
            strcpy(inputFile, argv[++i]);
        }
        else if (!strcmp(argv[i], "--output"))
        {
            strcpy(dirName, argv[++i]);
        }
        else if (!strcmp(argv[i], "--max_iter"))
        {
            if (!isCorrectInt(argv[i + 1]))
            {
                printf("Error: \"%s\" is not a integer number", argv[i + 1]);
                return 1;
            }
            maxIter = atoi(argv[++i]);
        }
        else if (!strcmp(argv[i], "--dump_freq"))
        {
            if (!isCorrectDouble(argv[i + 1]))
            {
                printf("Error: \"%s\" is not a correct number", argv[i + 1]);
                return 1;
            }
            dumpFreq = atof(argv[++i]);
        }
        else
        {
            printf("Error: invalid argument \"%s\"", argv[i]);
            return 1;
        }
    }

    if (strlen(inputFile) == 0)
    {
        printf("Error: input file address is not entered");
        return 1;
    }
    if (strlen(dirName) == 0)
    {
        printf("Error: output file address is not entered");
        return 1;
    }

    generate(inputFile, dirName, maxIter, dumpFreq);
    return 0;
}