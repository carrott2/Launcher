using System;
using System.IO;

namespace BedrockCosmos.App
{
    internal static class PathDefinitions
    {
        internal static string AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
        internal static string AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\";
        internal static string CosmosAppData = Path.Combine(AppDataDirectory, @"Bedrock Cosmos\");
        internal static string CustomJsonsDirectory = Path.Combine(CosmosAppData, @"CustomJsons\");
        internal static string MiscDirectory = Path.Combine(CosmosAppData, @"Misc\");
        internal static string ResponsesDirectory = Path.Combine(CosmosAppData, @"Responses-main\");
    }
}