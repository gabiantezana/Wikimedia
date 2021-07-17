using System.Collections.Generic;
using System.Threading.Tasks;
using Wikimedia.Core.Entities;

namespace Wikimedia.Core.Interfaces
{
    public interface IDataReader
    {
        Task<IEnumerable<WikimediaDataLine>> GetDataByUrl(string url);

    }
}
