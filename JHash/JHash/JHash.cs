using System;
using System.Collections.Generic;
using System.Text;

namespace JacHash
{
    public class JHash
    {
        private string text = "";
        private byte[] file;
        private int byteSize = 8;


        public JHash(string theString, int size = 8)
        {
            byteSize = size;
            text = theString;
	}

        public JHash(byte[] theFile, int size = 8)
        {
            byteSize = size;
            file = theFile;
        }

        public string GenerateFromString()
        {
            byte[] letters = Encoding.ASCII.GetBytes(text);
            byte[] hashed = mainHashing(letters);
            return getHexString(hashed);
        }

        public string GenerateFromFile()
        {
            byte[] hashed = mainHashing(file);
            return getHexString(hashed);
        }


        private string getHexString(byte[] bytes)
        {
            StringBuilder accum = new StringBuilder();
            for(int i = 0; i < bytes.Length; i++) 
            {
                accum.AppendFormat("{0:x2}", bytes[i]);
            }
            return accum.ToString();
        }

        private byte[] mainHashing(byte[] letters)
        {
            byte[] result = new byte[byteSize];
            byte[] input = pad(letters);

	    byte a = (byte)input.Length;
	    byte b = calcByte(input, 0, 1);
	    byte c = calcByte(input, 2, 3);
	    byte d = calcByte(input, 4, 5);
	    byte e = calcByte(input, 6, 7);

            for (int x = 0; x < input.Length; x++) 
            {
	        result[x % byteSize] = (byte)(a | (b | c) & (d ^ e));
		a |= b;
		b |= c;
		c |= d;
		d |= e;
            }

            return result;
        }

	private byte calcByte(byte[] input, int start, int end)
	{
	    return (byte)((input[start] / 2) + (input[end] / 2));
	}

        private byte[] pad(byte[] entered)
        {
            int origLength = entered.Length;
            if (entered.Length >= byteSize)
            {
                return entered;
            }
            byte[] result = new byte[byteSize];
            Array.Copy(entered, result, entered.Length);
            for (int x = entered.Length; x < byteSize; x++)
            {
                result[x] = 0x1F;
            }
            return result;
        }
    }
}
