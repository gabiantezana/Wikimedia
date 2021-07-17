using System.Collections.Generic;
using Wikimedia.Core.Entities;

namespace Wikimedia.Core.Interfaces
{
    public interface IResponse
    {
        void WriteResponse(IEnumerable<WikimediaDataLine> wikimediaData);
    }
}
