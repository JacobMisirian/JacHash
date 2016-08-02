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
        /// <summary>
        /// Gets a value indicating whether this instance hash length.
        /// </summary>
        /// <value><c>true</c> if this instance hash length; otherwise, <c>false</c>.</value>
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
            // Iterate over the data and fill the result buffer with incremental random numbers.
            for (int i = 0; i < data.Length; i++)
                result[i % HashLength] += prng(data[i]);
            // Turn the byte values into hex representation.
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
                ints[prng(b)]++;
            for (int i = 0; i < ints.Length; i++)
                Console.WriteLine("{0}\t{1}", i, ints[i]);
            for (int i = 0; i < ints.Length; i++)
                if (ints[i] == 0)
                    Console.WriteLine(i);
        }

        private void init(byte[] data)
        {
            // Sets the initial values for the prng to three constants and the length of the data.
            a = 0xBA;
            b = 0xDE;
            c = 0xFC;
            d = (byte)data.Length;

            // Sets te initial values for a and b so that they will exhibit the avalanche effect.
            foreach (byte by in data)
            {
                a = (a + prng(by)) % 255;
                b = (b + prng((byte)a)) % 255;
            }
            // Console.WriteLine("a {0}\nb {1}\nc {2}\nd {3}", a, b, c, d);
        }

        private byte[] pad(byte[] data)
        {
            // If the data is already long enough there is no padding needed.
            if (data.Length >= HashLength)
                return data;
            byte[] ret = new byte[HashLength];
            int i;
            // Copy the data into the new buffer.
            for (i = 0; i < data.Length; i++)
                ret[i] = data[i];
            // Add in more random data to fill the buffer.
            for (; i < HashLength; i++)
                ret[i] = prng((byte)i);
            return ret;
        }

        private byte prng(byte s)
        {
            // Scramble the prng numbers.
            a ^= (byte)(b | s ^ d);
            b ^= (byte)(c & s ^ a) + a;
            c ^= (byte)(d & s ^ c) + a;
            d ^= (byte)(a | s ^ a) + a;
            // Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", (byte)a, (byte)b, (byte)c, (byte)d, (byte)((s + b ^ c ^ d ^ a) + a));
            return (byte)((s + b ^ c ^ d ^ a) + a);
        }
    }
}