using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Json;

namespace ForgedCurse
{
    /// <summary>
    /// A section class containing methods to retrieve information for Addons files (releases)
    /// </summary>
    public sealed class FileSection : BaseSection
    {
        private const string API_ADDON_FILES = "addon/{0}/files/";
        private const string API_ADDON_FILE = "addon/{0}/file/{1}/";

        internal FileSection(ForgeClient client) : base(client)
        {
        }

        public Task<AddonRelease[]> RetrieveReleases(string id)
        {
            return HttpGetJson<AddonRelease[]>(string.Format(API_ADDON_FILES, id));
        }
    }
}
