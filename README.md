# JacHash
Hash algorithm in C#

## Using the Binary
The binary included is a complete executable for hashing with
JacHash. You can enter an repl where strings can be hashed and
displayed to the screen or provide a file path to find the
hash of the file.

Here is a complete list of flags:

Flag                  | Description
--------------------- | -----------
-f --file [PATH]      | Calculates the JacHash of [PATH].
-h --help             | Displays a help and exits.
-o --output [PATH]    | Redirects output to append to [PATH].
-r --repl             | Enters an REPL, where strings can be entered.

## Using the Library
JacHash currently comes in an exe form which can be included into
your project by adding a reference with the exe.

To make a new JacHash object, do the following:
```C#
JacHash hash = new JacHash();
```

Optionally you can specify the length of the hash by supplying an
integer into the constructor like such:
```C#
JacHash hash = new JacHash(32);
```

To hash a string you can do the following:
```C#
Console.WriteLine(hash.Hash(Encoding.ASCII.GetBytes(Console.ReadLine())));
```

The Hash() method takes in a byte[] and returns a hex string
with the hash.
