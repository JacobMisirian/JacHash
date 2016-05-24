using System;
using System.IO;
using System.Text;

namespace JacHash
{
    public class JacHash
    {
        /// <summary>
        /// The MAX LENGTH.
        /// </summary>
        public int MAX_LENGTH = 17;

        // These are the initial register values.
        private uint a = 0x6B87;
        private uint b = 0x7F43;
        private uint c = 0xA4AD;
        private uint d = 0xDC3F;

        // This is a register that is dependent on all of the bytes. Responsible for avalanche effect.
        private uint x = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="JacHash.JacHash"/> class.
        /// </summary>
        public JacHash() {}
        /// <summary>
        /// Initializes a new instance of the <see cref="JacHash.JacHash"/> class.
        /// </summary>
        /// <param name="maxLength">Max length of result hash.</param>
        public JacHash(int maxLength)
        {
            MAX_LENGTH = maxLength;
        }
        /// <summary>
        /// Creates a hash from a stream.
        /// </summary>
        /// <returns>The hash from the stream.</returns>
        /// <param name="stream">Stream.</param>
        public string Hash(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);
            // Read the byte to send to the main Hash function.
            byte[] source = reader.ReadBytes((int)reader.BaseStream.Length);

            return Hash(source);
        }
        /// <summary>
        /// Creates a hash from a string.
        /// </summary>
        /// <returns>The hash from the string.</returns>
        /// <param name="text">Text.</param>
        public string Hash(string text)
        {
            byte[] source = new byte[text.Length];
            // Create the byte[] to send the the main Hash function.
            for (int i = 0; i < source.Length; i++)
                source[i] = (byte)text[i];
            return Hash(source);
        }
        /// <summary>
        /// Creates a hash from a byte array.
        /// </summary>
        /// <returns>The hash from the byte array.</returns>
        /// <param name="source">Source.</param>
        public string Hash(byte[] source)
        {
            // If the source bytes are less than the minimum MAX_LENGTH, pad() will append 0xFF until it hits MAX_LENGTH.
            source = pad(source);

            // The resulting hash.
            byte[] result = new byte[MAX_LENGTH];

            // This creates the avalanche effect by setting the value of x in a manner where all the bytes get to participate.
            foreach (byte b in source)
                x += b;
            // Loop through all the bytes and preform bitwise operations on them to randomize them.
            // Use i % MAX_LENGTH so that it will not go outside the range of 0 .. MAX_LENGTH.
            for (int i = 0; i < source.Length; i++)
                result[i % MAX_LENGTH] = transformByte(source[i]);

            // Return the result formatted to a hex format.
            return getHexString(result);
        }

        private byte transformByte(byte bl)
        {
            // This is the math that is preformed on the byte. Notice every calculation is dependant upon 'x'
            // which is dependant upon every byte in the source.
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
            // If we already meet the minimum length requirements, simply return.
            if (bytes.Length >= MAX_LENGTH)
                return bytes;
            
            byte[] ret = new byte[MAX_LENGTH];
            // Copy the bytes from the source into our new byte[].
            for (int i = 0; i < bytes.Length; i++)
                ret[i] = bytes[i];
            // Add in 0xFFs until we reach the maximum length.
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

