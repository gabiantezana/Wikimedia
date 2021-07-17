using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wikimedia.Core.Entities;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.Core
{
    public class Processor : IProcessor
    {
        private readonly IDataReader dataReader;
        private readonly IResponse response;
        private readonly IUrl url;
        private readonly IConfiguration configuration;

        public Processor(IDataReader dataReader, IResponse response, IUrl url, IConfiguration configuration)
        {
            this.dataReader = dataReader;
            this.response = response;
            this.url = url;
            this.configuration = configuration;
        }

        public async Task Process()
        {
            var hoursToProcess = int.Parse(configuration["WikimediaParams:HoursToProcess"]);
            Console.WriteLine($"Getting data from last {hoursToProcess} hours...");

            var urlList = GetUrlsFromHours(hoursToProcess);

            //Using semaphore because wikimedia server doesn't allow multiple request at the same time
            var semaphoreSlim = new SemaphoreSlim(2);

            var dataTasks = urlList.OrderByDescending(x=> x).Select((url) => Task.Run(async () =>
            {
                semaphoreSlim.Wait();
                Console.WriteLine($"Getting data from {url} ...");
                var rawData = await dataReader.GetDataByUrl(url);
                semaphoreSlim.Release();
                return rawData;
            })).ToArray();

            await Task.WhenAll(dataTasks);

            var allData = new List<WikimediaDataLine>();
            foreach (var item in dataTasks)
                allData.AddRange(item.Result);

            Console.WriteLine($"Calculating summary...");
            var summary = GetSummarized(allData);
            Console.WriteLine($"Summary finished...");

            response.WriteResponse(summary);

            Console.WriteLine($"The process has been completed");
        }

        public IEnumerable<WikimediaDataLine> GetSummarized(IEnumerable<WikimediaDataLine> wikiMediaRows)
        {
            var rowsToShow = int.Parse(configuration["WikimediaParams:RowsToShow"]);
            var myResult = wikiMediaRows.GroupBy(x => new { x.domain_code, x.page_title })
                                               .Select(y => new WikimediaDataLine()
                                               {
                                                   domain_code = y.Key.domain_code,
                                                   page_title = y.Key.page_title,
                                                   count_views = y.Sum(p => p.count_views)
                                               })
                                               .OrderByDescending(m => m.count_views).Take(rowsToShow).ToList();
            return myResult;
        }

        public List<string> GetUrlsFromHours(int hoursQuantity)
        {
            var urlList = new List<string>();

            var currentDateTime = DateTime.UtcNow;
            for (int i = 1; i <= (hoursQuantity); i++)
            {
                var dateTime = currentDateTime.AddHours(-i);
                urlList.Add(url.GetUrlFromDateTime(dateTime));
            }
            return urlList;
        }
    }

}
