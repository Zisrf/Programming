#include <stdio.h>
#include <string.h>
#include "uint1024_t.h"

int main(int argc, char *argv[])
{
    if (argc != 4)
    {
        printf("Error: invalid number of arguments");
        return 1;
    }

    struct uint1024_t A = fromStr(argv[1]);
    char sign = *argv[2];
    struct uint1024_t B = fromStr(argv[3]);

    if (sign == '+')
    {
        printfValue(addOp(A, B));
    }
    else if (sign == '-')
    {
        printfValue(subtrOp(A, B));
    }
    else if (sign == 'x')
    {
        printfValue(multOp(A, B));
    }
    else
    {
        printf("Error: invalid sign");
        return 1;
    }

    return 0;
}