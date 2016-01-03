using System;
using System.IO;
using System.Text;

namespace JacHash
{
    public class JacHash
    {
        public uint A { get { return a; } set { a = value; } }
        public uint B { get { return b; } set { b = value; } }
        public uint C { get { return c; } set { c = value; } }
        public uint D { get { return d; } set { d = value; } }

        private uint a = 0xBADA55;
        private uint b = 0x223344;
        private uint c = 0x152437;
        private uint d = 0x525234;
        private uint x;

        private int MAX_LENGTH;

        public JacHash(int MAX_LENGTH = 8)
        {
            this.MAX_LENGTH = MAX_LENGTH;
        }

        public JacHash(uint a, uint b, uint c, uint d, int MAX_LENGTH = 8)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.MAX_LENGTH = MAX_LENGTH;
        }

        public string Hash(string data)
        {
            return Hash(Encoding.ASCII.GetBytes(data));
        }

        public string Hash(byte[] data)
        {
            data = pad(data);
            byte[] result = new byte[MAX_LENGTH];
            for (int i = 0; i < data.Length; i++)
                result[i % MAX_LENGTH] = transformByte(data[i]);
            return getHexString(result);
        }

        public string Hash(Stream fileStream)
        {
            BinaryReader reader = new BinaryReader(fileStream);
            byte[] result = new byte[MAX_LENGTH];
            while (reader.BaseStream.Position < reader.BaseStream.Length)
                result[reader.BaseStream.Position % MAX_LENGTH] = transformByte(reader.ReadByte());
            return getHexString(result);
        }

        private byte[] pad(byte[] bytes)
        {
            int origLength = bytes.Length;
            if (bytes.Length >= MAX_LENGTH)
                return bytes;
            byte[] result = new byte[MAX_LENGTH];
            Array.Copy(bytes, result, bytes.Length);
            for (int j = bytes.Length; j < MAX_LENGTH; j++)
                result[j] = 0x1F;
            return result;
        }

        private byte transformByte(byte bl)
        {
            a = shiftLeft(bl, (int)(a + x));
            b = (a + bl);
            c = (a + b) | x;
            d ^= c;
            x = a ^ ((b | c) & d);
            a |= d;
            /*  Console.WriteLine("a: " + (byte)a);
            Console.WriteLine("b: " + (byte)b); 
            Console.WriteLine("c: " + (byte)c);
            Console.WriteLine("d: " + (byte)d);
            Console.WriteLine("x: " + (byte)x);*/
            bl = (byte)((a * c) + b - x * d + bl);
            return bl;
        }

        private byte shiftLeft(byte b, int bits)
        {
            return (byte)((byte)(b << bits) | (byte)(b >> 32 - bits));
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

