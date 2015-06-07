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
	

	    private string getHexString(byte[] bytes) {
	       StringBuilder accum = new StringBuilder();
	       for(int i = 0; i < bytes.Length; i++) {
	          accum.AppendFormat("{0:x2}", bytes[i]);
	       }
	       return accum.ToString();
	    }


        private byte[] mainHashing(byte[] letters)
        {
			byte[] result = new byte[byteSize];
			byte[] input = pad(letters);

            byte a = input[0];
            byte b = input[1];
            byte c = input[2];
            byte d = input[3];
            byte e = input[4];
            byte f = input[5];
            byte g = input[6];
            byte h = input[7];

            result[0] = (byte)((a & b) | (~a & c));
            result[1] = (byte)((a & c) | (b & ~c));
            result[2] = (byte)(a ^ b ^ c); 
            result[3] = (byte)(b ^ (a | ~c));
            result[4] = (byte)((d & e) | (~d & f));
            result[5] = (byte)((d & f) | (e & ~f));
            result[6] = (byte)(d ^ e ^ f);
            result[7] = (byte)(e ^ (h | ~g));


           /* for (int x = 0; x < result.Length; x++)
            {
                /*result[x % byteSize] = (byte)(result[x % byteSize] << 1);
                result[x % byteSize] |= input[(x * 2) % byteSize];
                result[x % byteSize] = (byte)(result[x % byteSize] >> 1);
                result[x % byteSize] ^= input[(Convert.ToInt32(Math.Sqrt(x))) % byteSize];
                result[x % byteSize] = (byte)(result[x % byteSize] >> 1);
                if (x < result.Length - 1)
                {
                    result[x % byteSize] *= (byte)(input[(x + 1) % byteSize] % byteSize);
                }


            }*/
			
			return result;
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