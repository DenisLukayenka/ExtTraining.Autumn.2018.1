using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    /// <summary>
    /// Model of book.
    /// </summary>
    public class Book : IFormattable, IEquatable<Book>, IComparable<Book>
    {
        /// <summary>
        /// Simple auto properties for parameters of book.
        /// </summary>
        public string Title { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }

        public string PublishingHouse { get; set; }

        public int Edition { get; set; }

        public int Pages { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(other, null))
            {
                throw new ArgumentNullException(nameof(other) + " should be not null.");
            }

            return (int)(this.Price - other.Price);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            Book book = obj as Book;

            return this.Equals(book);
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (other.Title == this.Title
                && other.Author == this.Author
                && other.Edition == this.Edition
                && other.Pages == this.Pages
                && other.Price == this.Price
                && other.Year == this.Year
                && other.PublishingHouse == this.PublishingHouse)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Overriden method ToString();
        /// </summary>
        /// <returns>
        /// String representation of book.
        /// </returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpper())
            {
                case "G":
                    return $"Book record: {Title}, {Price.ToString("C2", formatProvider)}";
                case "A":
                    return $"Book record: {Author}, {Title}, {Year}, {PublishingHouse}";
                case "B":
                    return $"Book record: {Author}, {Title}, {Year}";
                case "C":
                    return $"Book record: {Author}, {Title}";
                case "D":
                    return $"Book record: {Title}, {Year}, {PublishingHouse}";
                case "E":
                    return $"Book record: {Title}, {PublishingHouse}";
                case "F":
                    return
                        $"Book record: {Author}, {Title}, {Year}, {PublishingHouse}, ed.{Edition}, p.{Pages}, {Price.ToString("C2", formatProvider)}";

                default:
                    throw new FormatException(nameof(format) + " is invalid format.");
            }
        }
    }
}