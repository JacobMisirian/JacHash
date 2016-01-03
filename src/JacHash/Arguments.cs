using System;
using System.IO;

namespace JacHash
{
    public class Arguments
    {
        private string[] args { get; set; }
        private int position = 0;

        public Arguments(string[] args)
        {
            this.args = args;
        }

        public JacHashConfiguration Scan()
        {
            if (args.Length <= 0)
                DisplayHelp();
           
            JacHashConfiguration config = new JacHashConfiguration();

            for (position = 0; position < args.Length; position++)
            {
                switch (args[position].ToLower())
                {
                    case "-f":
                    case "--file":
                        config.JacHashMode = JacHashMode.File;
                        config.FilePath = expectData("[PATH]");
                        if (!File.Exists(config.FilePath))
                        {
                            Console.WriteLine("Input file " + config.FilePath + " does not exist!");
                            Environment.Exit(0);
                        }
                        return config;
                    case "-h":
                    case "--help":
                        DisplayHelp();
                        break;
                    case "-o":
                    case "--output":
                        config.OutputPath = expectData("[PATH]");
                        return config;
                    case "-r":
                    case "--repl":
                        config.JacHashMode = JacHashMode.Repl;
                        return config;
                    default:
                        Console.WriteLine("Unknown option: " + args[position]);
                        Environment.Exit(0);
                        break;
                }
            }
            return null;
        }

        private string expectData(string dataType)
        {
            if (args[++position].StartsWith("-"))
            {
                Console.WriteLine("Expected " + dataType + " after " + args[position - 1] + "!");
                Environment.Exit(0);
                return null;
            }
            else
                return args[position];
        }

        public void DisplayHelp()
        {
            Console.WriteLine("Usage: JacHash.exe [OPTIONS] [ARGS]");
            Console.WriteLine("Options:");
            Console.WriteLine("-f --file [PATH]    Calculates the hash of a file.");
            Console.WriteLine("-h --help           Displays this help and exits.");
            Console.WriteLine("-o --output [PATH]  Sets the output path for the result.");
            Console.WriteLine("-r --repl           Enters the REPL shell.");
            Environment.Exit(0);
        }
    }
}

