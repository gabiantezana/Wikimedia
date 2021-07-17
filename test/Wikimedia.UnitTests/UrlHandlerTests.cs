using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using Wikimedia.Core;

namespace Wikimedia.UnitTests
{
    [TestClass]
    public class UrlHandlerTests
    {
        private DateFormatHandler dateFormatHandler = null;
        private UrlHandler urlHandler = null;

        [TestInitialize]
        public void Initialize()
        {
            dateFormatHandler = A.Fake<DateFormatHandler>();
            urlHandler = new UrlHandler(dateFormatHandler);
        }

        [TestMethod]
        public void Given_a_valid_url_when_GetUrlFromDateTime_is_called_Then_returns_valid_url()
        {
            var date = DateTime.ParseExact("16/07/2021 01", "dd/MM/yyyy hh", CultureInfo.InvariantCulture);
            var expected = "https://dumps.wikimedia.org/other/pageviews/2021/2021-07/pageviews-20210716-010000.gz";
            var result = urlHandler.GetUrlFromDateTime(date);

            Assert.AreEqual(expected, result);
        }


    }
}
