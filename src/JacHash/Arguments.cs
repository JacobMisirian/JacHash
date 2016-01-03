using System;

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
            for (position = 0; position < args.Length; position++)
            {
                switch (args[position])
                {
                    case "-f":
                    case "--file":
                        return new JacHashConfiguration(JacHashMode.File, expectData("file path"));
                    case "-h":
                    case "--help":
                        DisplayHelp();
                        break;
                    case "-r":
                    case "--repl":
                        return new JacHashConfiguration(JacHashMode.Repl);
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
            Console.WriteLine("-r --repl           Enters the REPL shell.");
            Environment.Exit(0);
        }
    }
}

