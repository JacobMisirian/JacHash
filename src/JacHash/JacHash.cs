using System;
using System.IO;
using System.Text;

namespace JacHash
{
    public class JacHash
    {
        public const int MAX_LENGTH = 10;

        private uint a = 0xBADA55;
        private uint b = 0x1B1337;
        private uint c = 0xFFFFAB;
        private uint d = 0xBDFFFF;
        private uint x = 0;

        public JacHash()
        {
        }

        public string Hash(string text)
        {
            byte[] source = new byte[text.Length];

            for (int i = 0; i < source.Length; i++)
                source[i] = (byte)text[i];
            return Hash(source);
        }

        public string Hash(byte[] source)
        {
            source = pad(source);

            byte[] result = new byte[MAX_LENGTH];
            foreach (byte b in source)
            {
                x += b;
            }

            for (int i = 0; i < source.Length; i++)
                result[i % MAX_LENGTH] = transformByte(source[i]);

            return getHexString(result);
        }

        private byte transformByte(byte bl)
        {
            a = shiftLeft((uint)bl, x);
            b = (b ^ bl) - x;
            c = (a + b) & x;
            d ^= x - b;
            x ^= d;
            bl = (byte)((a * c) + b - x * d ^ bl);
            return bl;
        }

        private byte[] pad(byte[] bytes)
        {
            if (bytes.Length >= MAX_LENGTH)
                return bytes;
            byte[] ret = new byte[MAX_LENGTH];
            for (int i = 0; i < bytes.Length; i++)
                ret[i] = bytes[i];
            for (int i = bytes.Length; i < ret.Length; i++)
                ret[i] = 0xFF;
            return ret;
        }

        private uint shiftLeft(uint b, uint bits)
        {
            return (uint)(((byte)b << (byte)bits) | ((byte)b >> 32 - (byte)bits));
        }

        private string getHexString(byte[] bytes)
        {
            StringBuilder accum = new StringBuilder();
            for(int i = 0; i < bytes.Length; i++)
                accum.AppendFormat("{0:x2}", bytes[i]);
            return accum.ToString();
        }
    }
}

