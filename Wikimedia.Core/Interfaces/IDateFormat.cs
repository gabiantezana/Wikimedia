using System;
using Wikimedia.Core.Entities;

namespace Wikimedia.Core.Interfaces
{
    public interface IDateFormat
    {
        DateFormat GetFormattedDate(DateTime dateTime);
    }
}
