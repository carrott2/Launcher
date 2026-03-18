using Titanium.Web.Proxy.EventArguments;

// =============================================================================
// Bedrock Cosmos - Copyright (c) 2026
//
// This file is part of Bedrock Cosmos, licensed under the MIT License.
// You must read and agree to the terms of the MIT License before using,
// copying, modifying, or distributing this code.
//
// MIT License - Full terms: https://opensource.org/licenses/MIT
// =============================================================================

namespace BedrockCosmos.Proxy
{
    public static class ProxyEventArgsBaseExtensions
    {
        public static ProxyClientState GetState(this ProxyEventArgsBase args)
        {
            if (args.ClientUserData == null) args.ClientUserData = new ProxyClientState();

            return (ProxyClientState)args.ClientUserData;
        }
    }
}