using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Json;
using ForgedCurse.Utility;

namespace ForgedCurse
{
    /// <summary>
    /// A section class containing methods to retrieve information for Addons files (releases)
    /// </summary>
    public sealed class FileSection : BaseSection
    {
        private const string API_ADDON_FILES = "addon/{0}/files/";
        private const string API_ADDON_FILE = "addon/{0}/file/{1}/";
        private const string API_ADDON_HASH = "fingerprint/";

        internal FileSection(ForgeClient client) : base(client)
        {
        }

        public Task<AddonRelease[]> RetrieveReleases(int id)
        {
            return HttpGetJson<AddonRelease[]>(string.Format(API_ADDON_FILES, id));
        }

        public Task<AddonRelease> RetrieveRelease(int id, int releaseId)
        {
            return HttpGetJson<AddonRelease>(string.Format(API_ADDON_FILE, id, releaseId));
        }

        public Task<HashSearchResult> SearchHashes(params uint[] hashes)
        {
            return HttpPostJson<HashSearchResult>(API_ADDON_HASH, JsonContent.FromObject(hashes));
        }
    }
}
