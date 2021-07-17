using System;
using System.Globalization;
using Wikimedia.Core.Entities;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.Core
{
    public class DateFormatHandler : IDateFormat
    {
        public DateFormat GetFormattedDate(DateTime dateTime)
        {
            return new DateFormat()
            {
                year = dateTime.ToString("yyyy", CultureInfo.InvariantCulture),
                month = dateTime.ToString("MM", CultureInfo.InvariantCulture),
                day = dateTime.ToString("dd", CultureInfo.InvariantCulture),
                hours = dateTime.ToString("HH", CultureInfo.InvariantCulture),
                htt = dateTime.ToString("htt", CultureInfo.InvariantCulture)
            };
        }
    }
}
