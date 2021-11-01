#include <stdio.h>
#include <stdint.h>
#include <string.h>

struct uint1024_t
{
    uint8_t digit[128];
};

int isNull(struct uint1024_t x)
{
    for (int i = 0; i < 128; ++i)
        if (x.digit[i] != 0)
            return 0;
    return 1;
}

struct uint1024_t bigNull()
{
    struct uint1024_t res;
    for (int i = 0; i < 128; ++i)
        res.digit[i] = 0;
    return res;
}

struct uint1024_t fromUint(uint32_t x)
{
    struct uint1024_t res = bigNull();
    int i = 0;
    while (x > 0)
    {
        res.digit[i] = x % 256;
        x /= 256;
        ++i;
    }
    return res;
}

struct uint1024_t addOp(struct uint1024_t x, struct uint1024_t y)
{
    int sum, delta = 0;
    for (int i = 0; i < 128; ++i)
    {
        sum = (int)x.digit[i] + (int)y.digit[i] + delta;
        delta = sum / 256;
        sum %= 256;
        x.digit[i] = sum;
    }
    return x;
}

struct uint1024_t subtrOp(struct uint1024_t x, struct uint1024_t y)
{
    int diff, delta = 0;
    for (int i = 0; i < 128; ++i)
    {
        diff = (int)x.digit[i] - (int)y.digit[i] - delta;
        delta = 0;
        if (diff < 0)
        {
            delta = 1;
            diff += 256;
        }
        x.digit[i] = diff;
    }
    return x;
}

struct uint1024_t mult(struct uint1024_t x, int y)
{
    int mult, delta = 0;
    for (int i = 0; i < 128; ++i)
    {
        mult = x.digit[i] * y + delta;
        delta = mult / 256;
        mult %= 256;
        x.digit[i] = mult;
    }
    return x;
}

struct uint1024_t shiftLeftOp(struct uint1024_t x, int k)
{
    for (int i = 127; i >= k; --i)
        x.digit[i] = x.digit[i - k];
    for (int i = 0; i < k; ++i)
        x.digit[i] = 0;
    return x;
}

struct uint1024_t multOp(struct uint1024_t x, struct uint1024_t y)
{
    struct uint1024_t res = bigNull();
    struct uint1024_t mults[128];
    for (int i = 0; i < 128; ++i)
        mults[i] = shiftLeftOp(mult(x, y.digit[i]), i);
    for (int i = 0; i < 128; ++i)
        res = addOp(res, mults[i]);
    return res;
}

struct uint1024_t divOp(struct uint1024_t x, int y)
{
    struct uint1024_t res;
    int delta = 0;
    for (int i = 127; i >= 0; --i)
    {
        delta += x.digit[i];
        res.digit[i] = delta / y;
        delta %= y;
        delta *= 256;
    }
    return res;
}

int modOp(struct uint1024_t x, int y)
{
    int delta = 0;
    for (int i = 127; i >= 0; --i)
    {
        delta += x.digit[i];
        delta %= y;
        delta *= 256;
    }
    return delta / 256;
}

void printfValue(struct uint1024_t x)
{
    if (isNull(x))
    {
        printf("0\n");
        return;
    }
    char digits[309];
    int i = 0;
    while (!isNull(x))
    {
        digits[i] = modOp(x, 10) + '0';
        x = divOp(x, 10);
        ++i;
    }
    int isStarted = 0;
    for (int j = i - 1; j >= 0; --j)
    {
        if (digits[j] != '0')
            isStarted = 1;
        if (isStarted)
            printf("%c", digits[j]);
    }
    printf("\n");
}

void scanfValue(struct uint1024_t *x)
{
    *x = bigNull();
    char str[309];
    scanf("%s", str);
    for (int i = 0; str[i] != '\0'; ++i)
    {
        *x = mult(*x, 10);
        *x = addOp(*x, fromUint(str[i] - '0'));
    }
}

int main(int argc, char *argv[])
{
    struct uint1024_t A;
    struct uint1024_t B;

    printf("A = ");
    scanfValue(&A);
    printf("B = ");
    scanfValue(&B);

    printf("A + B = ");
    printfValue(addOp(A, B));

    printf("A - B = ");
    printfValue(subtrOp(A, B));

    printf("A * B = ");
    printfValue(multOp(A, B));

    int x, n;
    printf("x = ");
    scanf("%d", &x);
    printf("n = ");
    scanf("%d", &n);
    A = fromUint(1);
    B = fromUint(x);
    for (int i = 0; i < n; ++i)
        A = multOp(A, B);
    printf("x ^ n = ");
    printfValue(A);
    return 0;
}