using System;

namespace JacHash
{
    public enum JacHashMode
    {
        Repl,
        File
    }

    public class JacHashConfiguration
    {
        public JacHashMode JacHashMode { get; set; }
        public string FilePath { get; set; }
        public string OutputPath { get; set; }
        public int Length { get { return length; } set { length = value; } }
        private int length = 32;
    }
}

