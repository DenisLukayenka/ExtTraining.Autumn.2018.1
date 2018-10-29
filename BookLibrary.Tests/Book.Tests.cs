﻿using System;
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
    public class Class1
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
        [TestCase("F", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019, Manning, ed.4, p.900, 40,00 ₽")]
        [TestCase("B", ExpectedResult = "Book record: Jon Skeet, C# in Depth, 2019")]
        public string ToString_FormatsWithoutCultureInfo(string format)
            => book.ToString(format);
    }
    //- Book record: Jon Skeet, C# in Depth, 2019, "Manning", 
    //- Book record: Jon Skeet, C# in Depth, 2019
    //- Book record: Jon Skeet, C# in Depth
    //- Book record: C# in Depth, 2019, "Manning"
    //- Book record: C# in Depth и т.д.
}