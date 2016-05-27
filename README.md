# JacHash
Hash algorithm in C#, C, and C++. JacHash can be used on strings, byte arrays, and Streams.
The algorithm works to the best of my testing abilities, and simulates the avalanche
effect, meaning that even a slight alteration of an input will cause a drastically
different output.

## Using the C# Binary
The binary included is a complete executable for hashing with
JacHash. You can enter an repl where strings can be hashed and
displayed to the screen.

Here is a complete list of flags:

Flag                  | Description
--------------------- | -----------
-f --file [PATH]      | Calculates the JacHash of [PATH].
-h --help             | Displays a help and exits.
-l --length [LENGTH]  | Sets the length in bytes to [LENGTH].
-o --output [PATH]    | Redirects output to append to [PATH].
-r --repl             | Enters an REPL, where strings can be entered.

## Using the C# Library
JacHash currently comes in an exe form which can be included into
your project by adding a reference with the exe. The JacHash class
implements the ```C# System.Security.Cryptography.HashAlgorithm ```.

To make a new JacHash object, do the following:
```C#
JacHash hash = new JacHash();
```

Optionally you can specify the length of the hash by supplying an
integer into the constructor like such:
```C#
JacHash jacHash = new JacHash(32);
```

To hash a string you can do the following:
```C#
Console.WriteLine(jacHash.ComputeHashString("helloworld"));

## Using the C library

This algorithm is also written in the C programming language. To use,
you must first include jachash.c, jachash.h, and jachash_context.h files
into your project. Then in your main.c file you can write ```C #include "jachash.h"```.
and ```C #include "jachash_context.h". From there you make a new context by doing
```C struct jachash_context context;```. The computeFromBytes, string, and stream
methods all take in a ```C char* dest``` argument, this is a pointer to
an area of RAM where the hash will be written to, here is a complete C file
to hash a string:
```C
#include "jachash.h"
#include "jachash_context.h"

int main () {
	struct jachash_context context;
	char hash [25];
	computeHashFromString (context, hash, "hello world!");
	printf("%s\n", hash);
}
```

## Using the C++ library

The C++ implementation contains a lot of the same code and using as 
the C implementation, but is in the form of a class. Here's that same
program in C++:
```C++
#include <string.h>
#include "jachash.h"

using namespace std;

int main(int argc, char* argv[]) {
    jachash* hash = new jachash();
    char hashStr[25];
    hash->computeHashFromString(hashStr, "hello world!");
    printf("%s\n", hashStr);
    return 0;
}
```
