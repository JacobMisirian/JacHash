using System;
using System.Collections.Generic;
using System.Text;

namespace JacHash
{
    public class JHash
    {
        private string text = "";
        private byte[] file;
		
		public JHash(string theString)
        {
            text = theString;
        }
		
		public JHash(byte[] theFile)
		{
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
			byte[] result = new byte[8]{0x6A, 0x61, 0x63, 0x6F, 0x62, 0x67, 0x67, 0x67};
			byte[] input = pad(letters);
			
			for (int x = 0; x < result.Length; x++)
			{
				result[x % 8] = (byte)(result[x % 8] << 1);
				result[x % 8] ^= input[(x * 2) % 8];
				result[x % 8] = (byte)(result[x % 8] >> 1);
				result[x % 8] ^= input[(Convert.ToInt32(Math.Sqrt(x))) % 8];
				result[x % 8] = (byte)(result[x % 8] << 1);
			}
			
			return result;
        }
		
		private byte[] pad(byte[] entered)
		{
			if (entered.Length >= 8)
			{
				return entered;
			}
			byte[] result = new byte[8];
			Array.Copy(entered, result, entered.Length);
			for (int x = entered.Length; x < 8; x++)
			{
				result[x] = 1;
			}
			return result;
		}
	}
}

