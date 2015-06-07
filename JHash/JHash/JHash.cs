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
			
			for (int x = 0; x < result.Length; x++)
			{
				result[x % byteSize] = (byte)(result[x % byteSize] << 1);
				result[x % byteSize] ^= input[(x * 2) % byteSize];
				result[x % byteSize] = (byte)(result[x % byteSize] >> 1);
				result[x % byteSize] ^= input[(Convert.ToInt32(Math.Sqrt(x))) % byteSize];
				result[x % byteSize] = (byte)(result[x % byteSize] << 1);
			}
			
			return result;
        }
		
		private byte[] pad(byte[] entered)
		{
			if (entered.Length >= byteSize)
			{
				return entered;
			}
			byte[] result = new byte[byteSize];
			Array.Copy(entered, result, entered.Length);
			for (int x = entered.Length; x < byteSize; x++)
			{
				result[x] = 1;
			}
			return result;
		}
	}
}