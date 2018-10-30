using System;
using System.Collections.Generic;
using System.Globalization;
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

            string currentPossibleSymbols = _possibleSymbols.Substring(0, @base);

            return FindDecimalValue(source.ToUpper(CultureInfo.InvariantCulture), currentPossibleSymbols, @base);
        }

        private static int FindDecimalValue(string source, string currentPossibleSymbols, int @base)
        {
            long powOfNumber = 1;
            int decimalValue = 0;

            for (int i = source.Length - 1; i >= 0; i--)
            {
                int numberOfSymbol = currentPossibleSymbols.IndexOf(source[i]);

                if (numberOfSymbol == -1)
                {
                    throw new ArgumentException(nameof(source) + " incorrect string representation.");
                }

                try
                {
                    checked
                    {
                        decimalValue += numberOfSymbol * (int)powOfNumber;
                    }

                    powOfNumber *= @base;
                }
                catch (OverflowException e)
                {
                    throw new ArgumentException(nameof(source) + " incorrect string representation.\n" +
                                                e.InnerException);
                }
            }

            return decimalValue;
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
        }
    }
}