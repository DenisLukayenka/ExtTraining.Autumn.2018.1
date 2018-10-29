using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StringExtension
{
    public static class Parser
    {
        private static string _possibleSymbols = "0123456789ABCDEF";

        public static int ToDecimal(this string source, int @base)
        {
            CheckArguments(source, @base);

            int lenght = source.Length - 1;
            int answer = 0;

            foreach (var symbol in source.ToUpper())
            {
                if (answer < 0)
                {
                    throw new ArgumentException();
                }

                int numberOfSymbol = _possibleSymbols.IndexOf(symbol);
                int tempNumber = numberOfSymbol * (int)Math.Pow(@base, lenght--);

                answer += tempNumber;
            }

            return answer;
        }

        private static void CheckArguments(string source, int @base)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source) + " should be not null.");
            }

            if (@base > 16)
            {
                throw new ArgumentOutOfRangeException(nameof(@base) + " can't be more than 16.");
            }

            if (@base < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(@base) + " can't be less than 2.");
            }

            if (HaveBadSymbols(source, @base))
            {
                throw new ArgumentException(nameof(source) + " bad string value.");
            }
        }

        private static bool HaveBadSymbols(string source, int @base)
        {
            bool flag = false;
            foreach (var symbol in source.ToUpper())
            {
                if (!IsCurrectSymbol(symbol, @base))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        private static bool IsCurrectSymbol(char symbol, int index)
        {  
            bool flag = false;
            for (int i = 0; i < index; i++)
            {
                if (symbol == _possibleSymbols[i])
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
    }
}
