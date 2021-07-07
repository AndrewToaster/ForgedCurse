using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ForgedCurse.Json;
using ForgedCurse.Utility;

namespace ForgedCurse
{
    /// <summary>
    /// A section class containing methods to retrieve information for Addons
    /// </summary>
    public sealed class AddonSection : BaseSection
    {
        private const string API_ADDONS = "addon/";
        private const string API_ADDON = "addon/{0}/";
        private const string API_ADDON_DESC = "addon/{0}/description/";

        internal AddonSection(ForgeClient client) : base(client)
        {
        }

        /// <summary>
        /// Retries the information about a addon specified using an id
        /// </summary>
        /// <param name="id">The identifier of the addon</param>
        /// <returns>Information about the addon</returns>
        public Task<Addon> RetriveAddon(int id)
        {
            return HttpGetJson<Addon>(string.Format(API_ADDON, id));
        }

        /// <summary>
        /// Retries the information about addons specified using an array of ids
        /// </summary>
        /// <param name="ids">The array containing the identifiers</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/></returns>
        public Task<Addon[]> RetriveAddons(params int[] ids)
        {
            return HttpPostJson<Addon[]>(API_ADDONS, JsonContent.FromObject(ids));
        }

        /// <summary>
        /// Retries the HTML-formatted description of the specified addon
        /// </summary>
        /// <param name="id">The identifier of the addon</param>
        /// <returns>HTML-formatted description of the addon</returns>
        public Task<string> RetriveDescription(int id)
        {
            return HttpString(string.Format(API_ADDON_DESC, id), _http.GetAsync);
        }
    }
}
