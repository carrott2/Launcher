using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using BedrockCosmos.App;

namespace BedrockCosmos
{
    public static class JsonData
    {
        public static List<string> AllowedUrls = null;
        public static List<Endpoint> MainPages = null;
        public static List<MarketItem> MarketItems = null;
        public static List<MarketItem> PackSearchIds = null;
        private static string jsonPath = PathDefinitions.ResponsesDirectory + @"LauncherJsons\";

        public static void InitializeJsons()
        {
            AllowedUrls =
            JsonConvert.DeserializeObject<List<string>>
            (File.ReadAllText(jsonPath + @"AllowedUrls.json"));

            MainPages =
            JsonConvert.DeserializeObject<List<Endpoint>>
            (File.ReadAllText(jsonPath + @"MainResponses.json"));

            MarketItems =
            JsonConvert.DeserializeObject<List<MarketItem>>
            (File.ReadAllText(jsonPath + @"PlayfabGetPublishItemResponses.json"));

            PackSearchIds =
            JsonConvert.DeserializeObject<List<MarketItem>>
            (File.ReadAllText(jsonPath + @"PlayfabSearchResponses.json"));
        }
    }
}
