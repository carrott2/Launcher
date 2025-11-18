using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrockCosmos
{
    public static class JsonData
    {
        public static List<string> AllowedUrls = 
            JsonConvert.DeserializeObject<List<string>>
            (File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Jsons\AllowedUrls.json"));

        public static List<Endpoint> MainPages =
            JsonConvert.DeserializeObject<List<Endpoint>>
            (File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Jsons\MainPages.json"));

        public static List<MarketItem> MarketItems =
            JsonConvert.DeserializeObject<List<MarketItem>>
            (File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Jsons\MarketItems.json"));
    }
}
