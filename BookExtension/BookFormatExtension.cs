using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;

namespace BookExtension
{
    public class BookFormatExtension : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;

        public BookFormatExtension() : this(CultureInfo.CurrentCulture)
        {
        }

        public BookFormatExtension(IFormatProvider p)
        {
            parent = p;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(Book))
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch
                {
                    throw new FormatException("error");
                }
            }

            if (string.IsNullOrWhiteSpace(format) || format.ToUpper(CultureInfo.InvariantCulture) != "Z")
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException)
                {
                    throw new FormatException($"The format of '{format}' is invalid.");
                }
            }

            Book book = (Book)arg;
            string price = book.Price.ToString("C2", parent);
            return $"Book record: {book.Title}, {book.Author}, {price}";
        }

        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}