using System;
using System.Collections.Generic;
using System.Text;

namespace JacHash
{
    public class JHash
    {
        private string text = "";
        public JHash(string theString)
        {
            text = theString;
        }

        public string Generate()
        {
            byte[] letters = GetLetters();
            string resultNum = mainHashing(letters).ToString();


            resultNum = preformPadding(resultNum);
            return resultNum.ToString();
        }

        private decimal mainHashing(byte[] letters)
        {
            decimal resultNum = 1;
            for (int x = 0; x < letters.Length; x++)
            {
                if (resultNum > 11264337593543950335)
                {
                    resultNum = Convert.ToDecimal(Convert.ToString(resultNum).Substring(0, Convert.ToString(resultNum).Length / 2));
                }
                else
                {
                    resultNum *= Convert.ToDecimal(Convert.ToString(letters[x], 2));
                }
            }

            return resultNum;
        }
        private byte[] GetLetters()
        {
            string ASCII = "";
            foreach (char c in text)
            {
                ASCII += System.Convert.ToInt32(c).ToString() + " ";
            }
            string[] tempLetters = ASCII.Split(' ');
            byte[] letters = new byte[tempLetters.Length - 1];
            for (int x = 0; x < tempLetters.Length - 1; x++)
            {
                letters[x] = Convert.ToByte(tempLetters[x]);
            }

            return letters;

        }

        public string preformPadding(string binary)
        {
            string result = binary;

            if (result.Length < 20)
            {
                for (int x = 0; result.Length < 20; x++)
                {
                    if (result.Length > x + 1)
                    {
                        result += result[x] + ((result[x + 1] + 1) * 2);
                    }
                    else
                    {
                        result += result[x];
                    }
                }
            }
            if (result.Length > 20)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result.ToString();
        }
    }
}

