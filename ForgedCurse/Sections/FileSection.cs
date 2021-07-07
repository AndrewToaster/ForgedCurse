using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Json;
using ForgedCurse.Utility;

namespace ForgedCurse.Sections
{
    /// <summary>
    /// A section class containing methods to retrieve information for addon's files (releases)
    /// </summary>
    public sealed class FileSection : BaseSection
    {
        private const string API_ADDON_FILES = "addon/{0}/files/";
        private const string API_ADDON_FILE = "addon/{0}/file/{1}/";
        private const string API_ADDON_HASH = "fingerprint/";
        private const string API_ADDON_FILE_CHANGE = "addon/{0}/file/{1}/changelog/";
        private const string API_ADDON_FILE_DOWNLOAD = "addon/{0}/file/{1}/download-url/";

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

        public Task<string> RetrieveChangelog(int id, int releaseId)
        {
            return HttpString(string.Format(API_ADDON_FILE_CHANGE, id, releaseId), _http.GetAsync);
        }

        public Task<string> RetrieveDownloadUrl(int id, int releaseId)
        {
            return HttpString(string.Format(API_ADDON_FILE_DOWNLOAD, id, releaseId), _http.GetAsync);
        }
    }
}
