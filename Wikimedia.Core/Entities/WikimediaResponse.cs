using System.Collections.Generic;

namespace Wikimedia.Core.Entities
{
    public class WikimediaResponse
    {
        public IEnumerable<WikimediaDataLine> WikimediaPageViewRows { get; private set; }
        public WikimediaResponse(IEnumerable<WikimediaDataLine> wikimediaRows)
        {
            WikimediaPageViewRows = wikimediaRows;
        }
    }
}
