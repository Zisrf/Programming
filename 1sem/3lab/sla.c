#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void parseTime(char *str, long long *time)
{
    int date;
    char month[4];
    int year;
    int hour;
    int minute;
    int second;
    char sign;
    int dh, dm;
    sscanf(str, "%*s - - [%d/%3s/%d:%d:%d:%d %c%2d%2d", &date, month, &year, &hour, &minute, &second, &sign, &dh, &dm);

    *time = (long long)year * 365 * 24 * 60 * 60 + date * 24 * 60 * 60 + hour * 60 * 60 + minute * 60 + second;

    if (!strcmp(month, "Feb"))
        *time += 31 * 24 * 60 * 60;
    else if (!strcmp(month, "Mar"))
        *time += 59 * 24 * 60 * 60;
    else if (!strcmp(month, "Apr"))
        *time += 90 * 24 * 60 * 60;
    else if (!strcmp(month, "May"))
        *time += 120 * 24 * 60 * 60;
    else if (!strcmp(month, "Jun"))
        *time += 151 * 24 * 60 * 60;
    else if (!strcmp(month, "Jul"))
        *time += 181 * 24 * 60 * 60;
    else if (!strcmp(month, "Aug"))
        *time += 212 * 24 * 60 * 60;
    else if (!strcmp(month, "Sep"))
        *time += 242 * 24 * 60 * 60;
    else if (!strcmp(month, "Oct"))
        *time += 273 * 24 * 60 * 60;
    else if (!strcmp(month, "Nov"))
        *time += 303 * 24 * 60 * 60;
    else if (!strcmp(month, "Dec"))
        *time += 334 * 24 * 60 * 60;

    if (sign == '-')
        *time -= dh * 60 * 60 + dm * 60;
    else if (sign == '+')
        *time += dh * 60 * 60 + dm * 60;

    *time += (year / 4) * 24 * 60 * 60;
    *time -= (year / 100) * 24 * 60 * 60;
    *time += (year / 400) * 24 * 60 * 60;
}

void parseCode(char *str, int *errorCode)
{
    *errorCode = 0;
    int lastQuotPos, len = 0;
    for (int i = 0; str[i] != '\0'; ++i)
    {
        ++len;
        if (str[i] == '\"')
            lastQuotPos = i;
    }
    for (int i = lastQuotPos + 2; i < len && str[i] >= '0' && str[i] <= '9'; ++i)
        *errorCode = 10 * (*errorCode) + str[i] - '0';
}

int main(int argc, char *argv[])
{
    if (argc != 3)
    {
        printf("Error: invalid number of arguments");
        return 1;
    }

    int dTime = atoi(argv[2]);
    FILE *inputFile = fopen(argv[1], "r");
    FILE *errors5xx = fopen("errors5xx", "w");
    if (!inputFile)
    {
        printf("Error: unable to open file \"%s\"", argv[1]);
        return 1;
    }
    if (!errors5xx)
    {
        printf("Error: unable to open file \"errors5xx\"");
        return 1;
    }

    int *t = malloc(sizeof(long long));
    int size = 1, p1 = 0, p2 = 0;
    int errorCode, maxRequestsCount = 0, errors5xxCount = 0;
    long long time;
    char str[1024];
    while (fgets(str, 1024, inputFile))
    {
        parseTime(str, &time);
        parseCode(str, &errorCode);

        if (errorCode / 100 == 5)
        {
            fprintf(errors5xx, "%s", str);
            ++errors5xxCount;
        }

        if (p2 == size)
        {
            t = realloc(t, 2 * size * sizeof(long long));
            size *= 2;
        }
        t[p2++] = time;
        while (p1 < p2 - 1 && t[p2 - 1] - t[p1] > dTime)
            ++p1;
        if (p2 - p1 > maxRequestsCount)
            maxRequestsCount = p2 - p1;
    }

    printf("Number of 5xx errors: %d (saved in \"errors5xx\" file)\n", errors5xxCount);
    printf("Maximum number of requests in %d seconds: %d", dTime, maxRequestsCount);

    free(t);
    fclose(inputFile);
    fclose(errors5xx);
    return 0;
}