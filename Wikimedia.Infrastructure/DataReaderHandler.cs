using ICSharpCode.SharpZipLib.GZip;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Wikimedia.Core.Entities;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.Infrastructure
{
    public class DataReaderHandler : IDataReader
    {
        private readonly IHttpClientFactory httpClient;
        public DataReaderHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory;
        }
        public async Task<IEnumerable<WikimediaDataLine>> GetDataByUrl(string url)
        {
            var contentStream = await DownloadFileAsync(url);
            return GetDataLinesFromStream(contentStream);
        }
        private async Task<Stream> DownloadFileAsync(string url)
        {
            var client = httpClient.CreateClient("wikimedia");
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var fileContent = await response.Content.ReadAsStreamAsync();
            var extracted = await ExtractGZip(fileContent);
            return extracted;
        }
        public async Task<Stream> ExtractGZip(Stream sm)
        {
            var extractedStream = new MemoryStream();
            using (GZipInputStream gzipStream = new GZipInputStream(sm))
            {
                await gzipStream.CopyToAsync(extractedStream, 4096);
            }
            extractedStream.Seek(0, SeekOrigin.Begin);
            return extractedStream;
        }
        private IEnumerable<WikimediaDataLine> GetDataLinesFromStream(Stream fileContentStream)
        {
            var rows = new List<WikimediaDataLine>();
            using (fileContentStream)
            using (var file = new StreamReader(fileContentStream))
                while (!file.EndOfStream)
                {
                    var splittedRow = file.ReadLine().Split(' ');
                    rows.Add(new WikimediaDataLine()
                    {
                        domain_code = splittedRow[0],
                        page_title = splittedRow[1],
                        count_views = int.Parse(splittedRow[2]),
                    });
                }
            return rows;
        }

    }
}
