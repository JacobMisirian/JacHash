using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JacHash
{
    public class JacHash : HashAlgorithm
    {
        /// <summary>
        /// The MAX LENGTH.
        /// </summary>
        public int MAX_LENGTH = 17;
        /// <summary>
        /// The FILLER_BYTE to pad the input.
        /// </summary>
        public const byte FILLER_BYTE = 0xFF;
        /// <summary>
        /// Gets a value indicating whether this instance hash.
        /// </summary>
        /// <value><c>true</c> if this instance hash; otherwise, <c>false</c>.</value>
        public new string Hash { get { return getHexString(hash); } }
        private byte[] hash;

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
            MAX_LENGTH = maxLength / 2;
        }
        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <returns>The hash.</returns>
        /// <param name="inputStream">Input stream.</param>
        public new byte[] ComputeHash(Stream inputStream)
        {
            Initialize();
            BinaryReader reader = new BinaryReader(inputStream);

            int appendToStream = 0;
            if (reader.BaseStream.Length < MAX_LENGTH)
                appendToStream = MAX_LENGTH - (int)reader.BaseStream.Length;
            byte[] result = new byte[MAX_LENGTH];

            while (reader.BaseStream.Position < reader.BaseStream.Length)
                x += reader.ReadBytes(1)[0];
            
            reader.BaseStream.Position = 0;
            for (int i = 0; i < appendToStream; i++)
                x += FILLER_BYTE;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
                result[reader.BaseStream.Position % MAX_LENGTH] = transformByte(reader.ReadBytes(1)[0]);
            for (int i = (int)reader.BaseStream.Length; i < ((int)reader.BaseStream.Length) + appendToStream; i++)
                result[i % MAX_LENGTH] = transformByte(FILLER_BYTE);
            hash = result;
            return result;
        }
        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <returns>The hash.</returns>
        /// <param name="buffer">Buffer.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="count">Count.</param>
        public new byte[] ComputeHash(byte[] buffer, int offset, int count)
        {
            Initialize();
            byte[] source = new byte[count - offset];
            for (int i = offset; i < count; i++)
                source[i - offset] = buffer[i];
            // If the source bytes are less than the minimum MAX_LENGTH, pad() will append FILLER_BYTE until it hits MAX_LENGTH.
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

            hash = result;
            return result;
        }
        /// <summary>
        /// Computes the hash string.
        /// </summary>
        /// <returns>The hash string.</returns>
        /// <param name="hash">Hash.</param>
        public string ComputeHashString(string hash)
        {
            ComputeHash(ASCIIEncoding.ASCII.GetBytes(hash));
            return Hash;
        }
        /// <summary>
        /// Computes the hash string.
        /// </summary>
        /// <returns>The hash string.</returns>
        /// <param name="inputStream">Input stream.</param>
        public string ComputeHashString(Stream inputStream)
        {
            ComputeHash(inputStream);
            return Hash;
        }
        /// <summary>
        /// Computes the hash string.
        /// </summary>
        /// <returns>The hash string.</returns>
        /// <param name="buffer">Buffer.</param>
        public string ComputeHashString(byte[] buffer)
        {
            ComputeHash(buffer);
            return Hash;
        }
        /// <summary>
        /// Computes the hash string.
        /// </summary>
        /// <returns>The hash string.</returns>
        /// <param name="buffer">Buffer.</param>
        /// <param name="offset">Offset.</param>
        /// <param name="count">Count.</param>
        public string ComputeHashString(byte[] buffer, int offset, int count)
        {
            ComputeHash(buffer, offset, count);
            return Hash;
        }
        /// <summary>
        /// Determines whether this instance hash core the specified array ibStart cbSize.
        /// </summary>
        /// <returns><c>true</c> if this instance hash core the specified array ibStart cbSize; otherwise, <c>false</c>.</returns>
        /// <param name="array">Array.</param>
        /// <param name="ibStart">Ib start.</param>
        /// <param name="cbSize">Cb size.</param>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            ComputeHash(array, ibStart, cbSize);
        }
        /// <summary>
        /// Determines whether this instance hash final.
        /// </summary>
        /// <returns><c>true</c> if this instance hash final; otherwise, <c>false</c>.</returns>
        protected override byte[] HashFinal()
        {
            return hash;
        }
        /// <Docs>A newly created instance doesn't have to be initialized.</Docs>
        /// <attribution license="cc4" from="Microsoft" modified="false"></attribution>
        /// <see cref="T:System.Security.Cryptography.HashAlgorithm"></see>
        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public override void Initialize()
        {
            a = 0x6B87;
            b = 0x7F43;
            c = 0xA4AD;
            d = 0xDC3F;
            x = 0;
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
            // Add in FILLER_BYTEs until we reach the maximum length.
            for (int i = bytes.Length; i < ret.Length; i++)
                ret[i] = FILLER_BYTE;
            
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

