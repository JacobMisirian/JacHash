#include <iostream>
#include "jachash.h"

using namespace std;

int main(int argc, char* argv[]) {
    jachash* hash = new jachash();
    hash->computeHashFromString("hello");
    return 0;
}