using System;
using System.Collections.Generic;
using System.Linq;
using Wikimedia.Core.Entities;
using Wikimedia.Core.Interfaces;

namespace Wikimedia.Infrastructure
{
    public class ResponseHandler : IResponse
    {
        public void WriteResponse(IEnumerable<WikimediaDataLine> wikimediaData)
        {
            wikimediaData.ToList().ForEach(x => Console.WriteLine($"{x.domain_code}\t{x.page_title}\t{x.count_views}")); ;
        }
    }
}
