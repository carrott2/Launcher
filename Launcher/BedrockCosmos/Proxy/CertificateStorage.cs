using System.IO;
using System.Security.Cryptography.X509Certificates;
using Titanium.Web.Proxy.Network;

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
    public class CertificateStorage : ICertificateCache
    {
        private readonly string _rootFolder;
        private readonly string _certsFolder;

        public CertificateStorage(string rootFolder)
        {
            _rootFolder = rootFolder;
            _certsFolder = Path.Combine(rootFolder, "Crts");
            Directory.CreateDirectory(_certsFolder);
        }

        public X509Certificate2 LoadRootCertificate(string pathOrName, string password, X509KeyStorageFlags storageFlags)
        {
            string path = Path.IsPathRooted(pathOrName)
                ? pathOrName
                : Path.Combine(_rootFolder, pathOrName);

            if (!File.Exists(path)) return null;

            return new X509Certificate2(path, password, storageFlags);
        }

        public void SaveRootCertificate(string pathOrName, string password, X509Certificate2 certificate)
        {
            string path = Path.IsPathRooted(pathOrName)
                ? pathOrName
                : Path.Combine(_rootFolder, pathOrName);

            File.WriteAllBytes(path, certificate.Export(X509ContentType.Pkcs12, password));
        }

        public X509Certificate2 LoadCertificate(string subjectName, X509KeyStorageFlags storageFlags)
        {
            string path = Path.Combine(_certsFolder, subjectName + ".pfx");
            if (!File.Exists(path)) return null;

            return new X509Certificate2(path, string.Empty, storageFlags);
        }

        public void SaveCertificate(string subjectName, X509Certificate2 certificate)
        {
            string path = Path.Combine(_certsFolder, subjectName + ".pfx");
            File.WriteAllBytes(path, certificate.Export(X509ContentType.Pkcs12));
        }

        public void Clear()
        {
            foreach (var file in Directory.GetFiles(_certsFolder, "*.pfx"))
                File.Delete(file);
        }
    }
}