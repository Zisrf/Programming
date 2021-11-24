#define M1 1000003
#define M2 1000033

struct set
{
    int *hashTable[M1];
    int size[M1];
    int realSize[M1];
    int p1, p2;
    int P1[M1], P2[M2];
};

void init(struct set *);

int exists(struct set *, char **, int, int);

void insert(struct set *, char **, int, int);

void del(struct set *);