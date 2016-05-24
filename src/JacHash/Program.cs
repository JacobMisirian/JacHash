using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JacHash
{
    class MainClass
    {
        static JacHash jacHash = new JacHash();
        static JacHashConfiguration config;

        public static void Main(string[] args)
        {
            config = new Arguments(args).Scan();
            switch (config.JacHashMode)
            {
                case JacHashMode.File:
                    processOutput(jacHash.Hash(new StreamReader(config.FilePath).BaseStream));
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
            return;
            if (config.OutputPath == "" || config.OutputPath == null)
                Console.WriteLine(output);
            else
                File.AppendAllText(config.OutputPath, output);
        }

        private static string hash(string text)
        {
            return jacHash.Hash(text);
        }

        private static string hash(byte[] data)
        {
            return jacHash.Hash(data);
        }
    }
}
