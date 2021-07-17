using System;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.Core
{

    public class UrlHandler : IUrl
    {
        private readonly IDateFormat dateFormat;
        public static string defaultUrl = "https://dumps.wikimedia.org/other/pageviews";

        public UrlHandler(IDateFormat dateFormat)
        {
            this.dateFormat = dateFormat;
        }
        public string GetUrlFromDateTime(DateTime dateTime)
        {
            var wikimediaDateTime = dateFormat.GetFormattedDate(dateTime);

            var fileName = $"pageviews-{wikimediaDateTime.year}{wikimediaDateTime.month}{wikimediaDateTime.day}-{wikimediaDateTime.hours}0000";
            var url = $"{defaultUrl}/{wikimediaDateTime.year}/{wikimediaDateTime.year}-{wikimediaDateTime.month}/{fileName}.gz";
            return url;
        }
    }
}
