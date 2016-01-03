using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JacHash
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            JacHash jacHash = new JacHash();
            Encoding encoding = Encoding.ASCII;
            JacHashConfiguration config = new Arguments(args).Scan();

            switch (config.JacHashMode)
            {
                case JacHashMode.File:
                    Console.WriteLine(jacHash.Hash(encoding.GetBytes(File.ReadAllText(config.FilePath))));
                    break;
                case JacHashMode.Repl:
                    while (true)
                    {
                        Console.Write("> ");
                        Console.WriteLine(jacHash.Hash(encoding.GetBytes(Console.ReadLine())));
                    }
            }
        }
    }
}
