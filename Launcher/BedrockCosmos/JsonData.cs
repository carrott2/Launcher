using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using BedrockCosmos.App;

// =============================================================================
// Bedrock Cosmos - Copyright (c) 2026
//
// This file is part of Bedrock Cosmos, licensed under the MIT License.
// You must read and agree to the terms of the MIT License before using,
// copying, modifying, or distributing this code.
//
// MIT License - Full terms: https://opensource.org/licenses/MIT
// =============================================================================

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
