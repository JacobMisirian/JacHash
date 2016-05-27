#include <iostream>
#include <string.h>
#include "jachash.h"

using namespace std;

int main(int argc, char* argv[]) {
    jachash* hash = new jachash();
    char hashStr[25];
    if (strcmp(argv[1], "-s") == 0)
        hash->computeHashFromString(hashStr, argv[2]);
    else if (strcmp(argv[1], "-f") == 0)
        hash->computeHashFromFile(hashStr, fopen(argv[2], "r"));
    printf("%s\n", hashStr);
    return 0;
}