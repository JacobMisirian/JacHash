using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JacHash
{
    class MainClass
    {
        static JacHash jacHash = new JacHash();
        static Encoding encoding = Encoding.ASCII;
        static JacHashConfiguration config;

        public static void Main(string[] args)
        {
            config = new Arguments(args).Scan();
            switch (config.JacHashMode)
            {
                case JacHashMode.File:
                    processOutput(hash(File.ReadAllText(config.FilePath)));
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
            if (config.OutputPath == "")
                Console.WriteLine(output);
            else
                File.AppendAllText(config.OutputPath, output);
        }

        private static string hash(string text)
        {
            return jacHash.Hash(encoding.GetBytes(text));
        }
    }
}
