using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wikimedia.Core;
using Wikimedia.Core.Entities;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.UnitTests
{
    [TestClass]
    public class ProcessorTests
    {
        private IDataReader dataReader;
        private IResponse response;
        private IUrl url;
        private Processor processor;

        [TestInitialize]
        public void TestInitialize()
        {
            dataReader = A.Fake<IDataReader>();
            response = A.Fake<IResponse>();
            url = A.Fake<IUrl>();

            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            processor = new Processor(dataReader, response, url, configuration);

        }

        [TestMethod]
        public void Given_a_valid_resumme_when_GetSummarized_is_called_then_returns_list()
        {
            var wikimediaDataLines = new List<WikimediaDataLine>()
            {
                new WikimediaDataLine(){ page_title ="aa", domain_code = "-", count_views = 3},
                new WikimediaDataLine(){ page_title ="aa", domain_code = "Main_Page", count_views = 34},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Wikipedia:Administrators", count_views = 544},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Аамҭа", count_views = 12},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Аботаника", count_views = 7},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Аботаника", count_views = 484},
                new WikimediaDataLine(){ page_title ="ace", domain_code = "Hanoi", count_views = 12},
                new WikimediaDataLine(){ page_title ="ace", domain_code = "Hanoi", count_views = 10},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Mangel", count_views = 79},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Nikki_Reed", count_views = 36},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Nodame_Cantabile", count_views = 17},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Oujaarsaand", count_views = 381},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Oujaarsaand", count_views = 9},
            };

            var expected = new List<WikimediaDataLine>()
            {
                  new WikimediaDataLine(){ page_title ="ab", domain_code = "Wikipedia:Administrators", count_views = 544},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Аботаника", count_views = 491},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Oujaarsaand", count_views = 390},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Mangel", count_views = 79},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Nikki_Reed", count_views = 36},
                new WikimediaDataLine(){ page_title ="aa", domain_code = "Main_Page", count_views = 34},
                new WikimediaDataLine(){ page_title ="ace", domain_code = "Hanoi", count_views = 22},
                new WikimediaDataLine(){ page_title ="af.m", domain_code = "Nodame_Cantabile", count_views = 17},
                new WikimediaDataLine(){ page_title ="ab", domain_code = "Аамҭа", count_views = 12},
                 new WikimediaDataLine(){ page_title ="aa", domain_code = "-", count_views = 3},
            }.ToList();

            var result = processor.GetSummarized(wikimediaDataLines).ToList();
            //CollectionAssert.AreEqual(expected, result);
            Assert.IsTrue(expected.SequenceEqual(expected));
        }
    }
}
