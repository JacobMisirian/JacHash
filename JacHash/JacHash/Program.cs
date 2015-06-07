using System;
using System.IO;

namespace JacHash
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			while (true)
			{
				Console.Write("Unsalted JHash, salted JHash, or JHash from file? u/s/f: ");
				string input = Console.ReadLine ();
				
				if (input == "s")
				{
					Console.Write ("Enter a string: ");
					string theString = Console.ReadLine ();
					Console.Write ("Enter a salt: ");
					Console.WriteLine(new JHash(theString + new JHash(Console.ReadLine()).GenerateFromString()).GenerateFromString());
        		}
				else if (input == "f")
				{
					Console.Write ("Enter a file location: ");
					byte[] file = File.ReadAllBytes(Console.ReadLine());
					Console.WriteLine (new JHash(file).GenerateFromFile());
				}
				else if (input == "us")
				{
					Console.Write ("Enter a string: ");
					string text = Console.ReadLine ();
					Console.Write ("Enter a length in bytes: ");
					Console.WriteLine(new JHash(text, Convert.ToInt32(Console.ReadLine())).GenerateFromString());
				}
				else
				{
					Console.Write ("Enter a string: ");
					Console.WriteLine(new JHash(Console.ReadLine ()).GenerateFromString());
				}
			}
		}
    }
}
