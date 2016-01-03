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
        public JacHashMode JacHashMode { get; private set; }
        public string FilePath { get; private set; }

        public JacHashConfiguration(JacHashMode jacHashMode, string filePath = "")
        {
            JacHashMode = jacHashMode;
            FilePath = filePath;
        }
    }
}

