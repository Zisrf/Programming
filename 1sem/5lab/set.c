#include <stdlib.h>
#include "set.h"

int getHash(char **cell, int h, int w, int *P, int M)
{
    int res = 0;
    for (int i = 0; i < h; ++i)
        for (int j = 0; j < w; ++j)
        {
            res += cell[i][j] * P[(i * h + j) % M];
            res %= M;
        }
    return res;
}

void init(struct set *s)
{
    s->p1 = 2;
    s->P1[0] = 1;
    for (int i = 1; i < M1; ++i)
    {
        s->P1[i] = (s->P1[i - 1] * s->p1) % M1;
        s->size[i] = 0;
        s->realSize[i] = 1;
        s->hashTable[i] = malloc(sizeof(int));
    }
    s->p2 = 3;
    s->P2[0] = 1;
    for (int i = 1; i < M2; ++i)
        s->P2[i] = (s->P2[i - 1] * s->p2) % M2;
}

int exists(struct set *s, char **cell, int h, int w)
{
    int h1 = getHash(cell, h, w, s->P1, M1);
    int h2 = getHash(cell, h, w, s->P2, M2);
    if (s->size[h1] == 0)
        return 0;
    for (int i = 0; i < s->size[h1]; ++i)
        if (s->hashTable[h1][i] == h2)
            return 1;
    return 0;
}

void insert(struct set *s, char **cell, int h, int w)
{
    int h1 = getHash(cell, h, w, s->P1, M1);
    int h2 = getHash(cell, h, w, s->P2, M2);
    if (s->size[h1] == s->realSize[h1])
    {
        s->realSize[h1] *= 2;
        s->hashTable[h1] = realloc(s->hashTable[h1], s->realSize[h1]);
    }
    s->hashTable[h1][s->size[h1]] = h2;
    ++s->size[h1];
}

void del(struct set *s)
{
    for (int i = 0; i < M1; ++i)
        free(s->hashTable[i]);
    free(s->hashTable);
}