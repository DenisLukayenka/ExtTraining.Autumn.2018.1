using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibrary;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BookExtension.Tests
{
    [TestFixture]
    public class BookFormatExtensionTests
    {
        private readonly Book book = new Book()
        {
            Title = "C# in Depth",
            Author = "Jon Skeet",
            Year = 2019,
            PublishingHouse = "Manning",
            Edition = 4,
            Pages = 900,
            Price = 40
        };

        [TestCase("{0:Z}", ExpectedResult = "Book record: C# in Depth, Jon Skeet, $40.00")]
        public string ToString_CustomFormatProvider(string format)
        {
            return string.Format(new BookFormatExtension(new CultureInfo("en-US")), format, book);
        }

        [Test]
        public void ToString_NotCorrectFormat_FormatException()
            => Assert.Throws<FormatException>(() => string.Format(new BookFormatExtension(), "{0:W}", book));
    }
}
