using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JacHash
{
    class MainClass
    {
        static JacHashConfiguration config;
        static JacHash jacHash;

        public static void Main(string[] args)
        {
            config = new Arguments(args).Scan();
            jacHash = new JacHash(config.Length);
            switch (config.JacHashMode)
            {
                case JacHashMode.File:
                    processOutput(jacHash.ComputeHashString(new StreamReader(config.FilePath).BaseStream));
                    break;
                case JacHashMode.Repl:
                    while (true)
                    {
                        processOutput("> ");
                        processOutput(hash(Console.ReadLine()));
                    }
            }
        }

        private static void processOutput(string output)
        {
            Console.WriteLine(output);
        }

        private static string hash(string text)
        {
            return jacHash.ComputeHashString(text);
        }

        private static string hash(byte[] data)
        {
            return jacHash.ComputeHashString(data);
        }
    }
}
