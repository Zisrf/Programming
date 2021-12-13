#include <stdint.h>

struct uint1024_t
{
    uint8_t digit[128];
};

int isNull(struct uint1024_t);

struct uint1024_t bigNull();

struct uint1024_t fromUint(uint32_t);

struct uint1024_t addOp(struct uint1024_t, struct uint1024_t);

struct uint1024_t subtrOp(struct uint1024_t, struct uint1024_t);

struct uint1024_t mult(struct uint1024_t, int);

struct uint1024_t shiftLeftOp(struct uint1024_t, int);

struct uint1024_t multOp(struct uint1024_t, struct uint1024_t);

struct uint1024_t divOp(struct uint1024_t, int);

int modOp(struct uint1024_t, int);

void printfValue(struct uint1024_t);

void scanfValue(struct uint1024_t *);

struct uint1024_t fromStr(char *);