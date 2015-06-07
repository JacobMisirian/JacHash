using System;

namespace JacHash
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Salted or unsalted hash: s/u ");
                if (Console.ReadLine() == "s")
                {
                    Console.Write("Enter in the string: ");
                    string text = new JHash(Console.ReadLine()).Generate();
                    Console.Write("Enter in the salt: ");
                    string salt = new JHash(Console.ReadLine()).Generate();
                    Console.WriteLine("Result is: " + new JHash(text + salt).Generate());
                }
                else
                {
                    Console.WriteLine("Enter in a string: ");
                    string theString = Console.ReadLine();
                    Console.WriteLine("Result is: " + new JHash(theString).Generate());
                }
            }
        }
    }
}
