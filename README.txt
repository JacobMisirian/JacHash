JacHash is a 64Bit hashing algorithm library in C#.

In the main folder there is two subfolders, named
JacHash and JHash. JacHash is a VS/mono project that
uses the libary as an example of using JacHash. The
other subfolder JHash is a VS/mono project which is
the libary (compiles to DLL) itslef. The class JHash
has 2 main methods, one to hash a string and one to
hash a byte[] (designed for hashing a file). Once the
DLL has been included, you can hash a string by doing
the following:

string theHash = new JHash(INPUT_STRING).GenerateFromString();

To hash a file you can also do the following:

byte[] contents = File.ReadAllBytes(PATH);
string theHash = new JHash(contents).GenerateFromFile();

That's it! It's just that simple! To include the DLL
you just right click on refrences and then choose the
DLL from your filesystem. Any questions can be directed
to misiriansoft@gmail.com or MisirianSoft on int0x10.com.


BONUS POINTS TO ANYONE WHO CAN CRACK THE HASH:
0a323e744c6c6440
