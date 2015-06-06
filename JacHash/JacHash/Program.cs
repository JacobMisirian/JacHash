using System;

namespace JacHash
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter in a string: ");
                string theString = Console.ReadLine();
                JHash hash = new JHash(theString);
                Console.WriteLine(hash.Generate());
            }
        }
    }
}
