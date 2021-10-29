#include <stdio.h>
#include <stdint.h>

struct uint1024_t
{
    uint8_t digit[128];
};

struct uint1024_t bigNull()
{
    struct uint1024_t res;
    for (int i = 0; i < 128; ++i)
        res.digit[i] = 0;
    return res;
}

struct uint1024_t addOp(struct uint1024_t x, struct uint1024_t y)
{
    int sum, delta = 0;
    for(int i = 0; i < 128; ++i)
    {
        sum = x.digit[i] + y.digit[i] + delta;
        x.digit[i] = sum % 256;
        delta = sum / 256;
    }
    return x;
}

struct uint1024_t subtrOp(struct uint1024_t x, struct uint1024_t y)
{
    int subtr, delta = 0;
    for(int i = 0; i < 128; ++i)
    {
        
    }
    return x;
}

int main(int argc, char *argv[])
{
    struct uint1024_t bigNum = bigNull();

    return 0;
}