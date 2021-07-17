using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Wikimedia.Core;
using Wikimedia.Core.Interfaces;
using Wikimedia.Infrastructure;

namespace Wikimedia.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = InitConfiguration();

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddScoped<IDataReader, DataReaderHandler>()
                .AddScoped<IDateFormat, DateFormatHandler> ()
                .AddScoped<IResponse, ResponseHandler>()
                .AddScoped<IUrl, UrlHandler>()
                .AddScoped<IProcessor, Processor>()
                .AddHttpClient()
                .BuildServiceProvider();


            var wikiMediaProcessor = serviceProvider.GetService<IProcessor>();
            await wikiMediaProcessor.Process();
        }

        public static IConfiguration InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

           return builder.Build();
        }
    }
}
