using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;
using ForgedCurse.Utility;

namespace ForgedCurse
{
    public static class Utilities
    {
        public static async Task<T> ParseJsonAsync<T>(this HttpResponseMessage response)
        {
            return CurseJSON.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<T> ParseJsonSafeAsync<T>(this HttpResponseMessage response)
        {
            return CurseJSON.Deserialize<T>(await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }

        public static Span<byte> GetNormalizedArray(Span<byte> bytes)
        {
            List<byte> normalized = new List<byte>();

            foreach (byte b in bytes)
            {
                if (!char.IsWhiteSpace(Convert.ToChar(b)))
                {
                    normalized.Add(b);
                }
            }

            return normalized.ToArray();
        }

        public static uint ComputeFingerprint(string jarFile)
        {
            if (!File.Exists(jarFile) || Path.GetExtension(jarFile) != ".jar")
                throw new ArgumentException("The specified path either doesn't exist or isn't pointing to a JAR file", nameof(jarFile));

            return MurmurHash2.Hash(File.ReadAllBytes(jarFile), 1);
        }

        public static string BuildAddonSearchUrl(string version = null, string name = null, int offset = 0, int amount = 10, AddonCategory category = AddonCategory.All, 
            AddonSorting sort = AddonSorting.Featured, AddonKind kind = AddonKind.Mod)
        {
            string url = $"https://addons-ecs.forgesvc.net/api/v2/addon/search?categoryId={category}&gameId=432&sort={sort}&index={offset}&pageSize={amount}&sectionId={kind}";

            if (!string.IsNullOrWhiteSpace(version))
                url += string.Format("&gameVersion={0}", version);

            if (!string.IsNullOrWhiteSpace(name))
                url += string.Format("&searchFilter={0}", name);

            return url;
        }
    }
}
