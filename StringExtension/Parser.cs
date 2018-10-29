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
        public static int ToDecimal(this string source, int @base)
        {
            CheckArguments(source, @base);



            return Convert(source, @base);
        }

        private static int Convert(string source, int @base)
        {
            int lenght = source.Length - 1;
            int answer = 0;

            foreach (var symbol in source.ToUpper())
            {
                int number = GetValueSymbol(symbol);
                int num = number * (int)Math.Pow(@base, lenght--);

                answer += num;
                if (answer < 0)
                {
                    throw new OverflowException();
                }
            }

            return answer;
        }

        private static int GetValueSymbol(char symbol)
        {
            Dictionary<char, int> numbers = new Dictionary<char, int>
            {
                { '0', 0 },
                { '1', 1 },
                { '2', 2 },
                { '3', 3 },
                { '4', 4 },
                { '5', 5 },
                { '6', 6 },
                { '7', 7 },
                { '8', 8 },
                { '9', 9 },
                { 'A', 10 },
                { 'B', 11 },
                { 'C', 12 },
                { 'D', 13 },
                { 'E', 14 },
                { 'F', 15 }
            };
            return numbers[symbol];
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
            string arraySymbols = "0123456789ABCDEF";

            bool flag = false;
            for (int i = 0; i <= index; i++)
            {
                if (symbol == arraySymbols[i])
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
    }
}
