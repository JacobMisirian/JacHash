using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacHash
{
    class Program
    {
        static void Main(string[] args)
        {
            JacHash hash = new JacHash();
            hash.TestBrute("0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz", 8);
            while (true)
            {
                Console.WriteLine(hash.Hash(ASCIIEncoding.ASCII.GetBytes(Console.ReadLine())));
            }
        }
    }
}
