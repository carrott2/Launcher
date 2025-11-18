using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockCosmos
{
    public class Endpoint
    {
        public string url { get; set; }
        public string response { get; set; }
    }

    public class MarketItem
    {
        public string uuid { get; set; }
        public string response { get; set; }
    }

    public class PlayfabGetPublishedItemBody
    {
        public string etag { get; set; }
        public string itemid { get; set; }
    }
}
