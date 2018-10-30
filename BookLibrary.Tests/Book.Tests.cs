using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static BookLibrary.Book;

namespace BookLibrary.Tests
{
    [TestFixture]
    public class Tester
    {
        private Book book = new Book()
        {
            Title = "C# in Depth",
            Author = "Jon Skeet",
            Year = 2019,
            PublishingHouse = "Manning",
            Edition = 4,
            Pages = 900,
            Price = 40
        };

        [TestCase("A", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019, Manning")]
        [TestCase("B", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019")]
        [TestCase("C", ExpectedResult = "Book record: Jon Skeet, C# in Depth")]
        [TestCase("D", ExpectedResult = "Book record: C# in Depth, 2019, Manning")]
        [TestCase("E", ExpectedResult = "Book record: C# in Depth, Manning")]
        [TestCase("G", ExpectedResult = "Book record: C# in Depth, $40.00")]
        public string ToString_FormatsWithoutCultureInfo(string format)
            => book.ToString(format, new CultureInfo("en-US"));

        [TestCase("{0:F}", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019, Manning, ed.4, p.900, $40.00")]
        public string ToString_FormatsWithCultureInfo_EnUS(string format)
        {
            return string.Format(new CultureInfo("en-US"), format, book);
        }

        [Test]
        public void Equal_TwoSameBooks_True()
        {
            Assert.IsTrue(book.Equals(book));
        }

        [Test]
        public void Equal_BookAndNll_False()
        {
            Assert.IsFalse(book.Equals(null));
        }

        [Test]
        public void Equal_TwoDifferentBooks_True()
        {
            Book book2 = new Book()
            {
                Title = "C# in Depth",
                Author = "Jon Skeet",
                Year = 2019,
                PublishingHouse = "Manning",
                Edition = 4,
                Pages = 900,
                Price = 40
            };

            bool actualResult = book.Equals(book2);
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void Equal_TwoDifferentBooks_False()
        {
            Book book2 = new Book()
            {
                Title = null,
                Author = "Jon Skeet",
                Year = 2019,
                PublishingHouse = "Manning",
                Edition = 4,
                Pages = 900,
                Price = 40
            };
            bool actualResult = book.Equals(book2);
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void CompareTo_TwoDifferentBooks_Zero()
        {
            int actualResult = book.CompareTo(book);
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CompareTo_TwoDifferentBooks_One()
        {
            Book book2 = new Book()
            {
                Title = null,
                Author = "Jon Skeet",
                Year = 2019,
                PublishingHouse = "Manning",
                Edition = 4,
                Pages = 900,
                Price = 35
            };
            int actualResult = book.CompareTo(book2);
            int expectedResult = 1;
            Assert.GreaterOrEqual(actualResult, expectedResult);
        }

        [Test]
        public void CompareTo_TwoDifferentBooks_MinusOne()
        {
            Book book2 = new Book()
            {
                Title = null,
                Author = "Jon Skeet",
                Year = 2019,
                PublishingHouse = "Manning",
                Edition = 4,
                Pages = 900,
                Price = 45
            };
            int actualResult = book.CompareTo(book2);
            int expectedResult = -1;
            Assert.LessOrEqual(actualResult, expectedResult);
        }

        [Test]
        public void ToString_NotCorrectFormat_FormatException()
            => Assert.Throws<FormatException>(() => string.Format("{0:W}", book));
    }
}