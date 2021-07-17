using System;

namespace Wikimedia.Core.Interfaces
{
    public interface IUrl
    {
        string GetUrlFromDateTime(DateTime dateTime);
    }
}
