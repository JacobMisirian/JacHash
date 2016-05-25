# JacHash
Hash algorithm in C#. JacHash can be used on strings, byte arrays, and Streams.
The algorithm works to the best of my testing abilities, and simulates the avalanche
effect, meaning that even a slight alteration of an input will cause a drastically
different output.

## Using the Binary
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

## Using the Library
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
```
