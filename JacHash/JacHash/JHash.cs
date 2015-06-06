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
            string ASCII = "";
            foreach (char c in text)
            {
                ASCII += System.Convert.ToInt32(c).ToString() + " ";
            }
            string[] tempLetters = ASCII.Split(' ');
            int[] letters = new int[tempLetters.Length - 1];
            for (int x = 0; x < tempLetters.Length - 1; x++)
            {
                letters[x] = Convert.ToInt32(tempLetters[x]);
            }

            decimal resultNum = 1;
            for (int x = 0; x < letters.Length; x++)
            {
                if (resultNum > 264337593543950335)
                {
                    resultNum = Convert.ToDecimal(Convert.ToString(resultNum).Substring(0, Convert.ToString(resultNum).Length / 2));
                }
                else
                {
                    resultNum *= Convert.ToDecimal(Convert.ToString(letters[x], 2));
                }
            }

            resultNum = preformPadding(resultNum);
            return resultNum.ToString();
        }

        public decimal preformPadding(decimal binary)
        {
            string result = binary.ToString();

            if (result.Length < 20)
            {
                for (int x = 0; result.Length < 20; x++)
                {
                    result += result[x];
                }
            }
            if (result.Length > 20)
            {
                result = result.Substring(0, 19);
            }

            return Convert.ToDecimal(result);
        }
    }
}

