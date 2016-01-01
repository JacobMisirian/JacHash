using System;
using System.Text;

namespace JacHash
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            JacHash jacHash = new JacHash();
            Encoding encoding = Encoding.ASCII;
            while (true)
            {
                Console.Write("> ");
                Console.WriteLine(jacHash.Hash(encoding.GetBytes(Console.ReadLine())));
            }
        }
    }
}
