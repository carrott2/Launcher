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

    public class PlayfabGetSearchedItemBody
    {
        public bool count { get; set; }
        public string filter { get; set; }
        public string orderBy { get; set; }
        public string scid { get; set; }
        public int top { get; set; }
    }
}
