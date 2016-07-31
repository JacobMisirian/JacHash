using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacHash
{
    /// <summary>
    /// JacHash.
    /// </summary>
    public class JacHash
    {
        public int HashLength { get; private set; }
        private uint a;
        private uint b;
        private uint c;
        private uint d;
        /// <summary>
        /// Initializes a new instance of the <see cref="JacHash.JacHash"/> class.
        /// </summary>
        /// <param name="hashLength">Hash length.</param>
        public JacHash(int hashLength = 16)
        {
            HashLength = hashLength;
        }
        /// <summary>
        /// Determines whether this instance hash  data.
        /// </summary>
        /// <returns><c>true</c> if this instance hash data; otherwise, <c>false</c>.</returns>
        /// <param name="data">Data.</param>
        public string Hash(byte[] data)
        {
            data = pad(data);
            init(data);
            byte[] result = new byte[HashLength];
            for (int i = 0; i < data.Length; i++)
                result[i % HashLength] += prng(data[i]);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                sb.AppendFormat("{0:x2}", result[i]);
            return sb.ToString();
        }
        /// <summary>
        /// Tests the brute.
        /// </summary>
        /// <param name="letters">Letters.</param>
        /// <param name="maxLength">Max length.</param>
        public void TestBrute(string letters, int maxLength)
        {
            Dictionary<string, string> hashes = new Dictionary<string, string>();
            char firstLetter = letters.First();
            char lastLetter = letters.Last();

            for (int length = 1; length < maxLength; ++length)
            {
                StringBuilder accum = new StringBuilder(new String(firstLetter, length));
                while (true)
                {
                    if (accum.ToString().All(val => val == lastLetter))
                        break;
                    for (int i = length - 1; i >= 0; --i)
                        if (accum[i] != lastLetter)
                        {
                            accum[i] = letters[letters.IndexOf(accum[i]) + 1];
                            break;
                        }
                        else
                            accum[i] = firstLetter;
                    string hash = Hash(System.Text.ASCIIEncoding.ASCII.GetBytes(accum.ToString()));
                    if (hashes.ContainsKey(hash))
                        Console.WriteLine("MATCH: {0}\t{1}", accum.ToString(), hashes[hash]);
                    else
                        hashes.Add(hash, accum.ToString());
                }
            }
        }
        /// <summary>
        /// Tests the prng.
        /// </summary>
        /// <param name="data">Data.</param>
        public void TestPrng(byte[] data)
        {
            int[] ints = new int[256];
            init(data);
            foreach(byte b in data)
            {
                ints[prng(b)]++;
            }
            for (int i = 0; i < ints.Length; i++)
                Console.WriteLine("{0}\t{1}", i, ints[i]);
            for (int i = 0; i < ints.Length; i++)
                if (ints[i] == 0)
                    Console.WriteLine(i);
        }

        private void init(byte[] data)
        {
            a = 0xBA;
            b = 0xBE;
            c = 0xCC;
            d = (byte)data.Length;

            foreach (byte by in data)
            {
                a ^= by;
           //     b -= by;
            }
        }

        private byte[] pad(byte[] data)
        {
            if (data.Length >= HashLength)
                return data;
            byte[] ret = new byte[HashLength];
            int i;
            for (i = 0; i < data.Length; i++)
                ret[i] = data[i];
            for (; i < HashLength; i++)
                ret[i] = (byte)i;
            return ret;
        }

        private byte prng(byte s)
        {
            a ^= (byte)(d ^ s | b);
            b ^= (byte)(d | s ^ a) + a;
            c ^= (byte)(s & b ^ a) + a;
            d ^= (byte)(a | s ^ b) + a;
            //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", (byte)a, (byte)b, (byte)c, (byte)d, (byte)(s * b + c - d * a));
            return (byte)(s + b ^ c ^ d ^ a);
        }
    }
}