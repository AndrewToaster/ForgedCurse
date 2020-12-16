using ForgedCurse.Enumeration;
using ForgedCurse.Utility;
using Newtonsoft.Json;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ForgedCurse
{
    /// <summary>
    /// Main class for interacting with the CurseForge API
    /// </summary>
    /// <remarks>
    /// Documentation taken from 'https://twitchappapi.docs.apiary.io/'
    /// </remarks>
    public class ForgeClient : IDisposable
    {
        private const string API_URL = "https://addons-ecs.forgesvc.net/api/v2";
        private const string API_MC_VERSIONS = "minecraft/version/";
        private const string API_MC_VERSION = "minecraft/version/{0}/";
        private const string API_FML_VERSIONS = "minecraft/modloader/";
        private const string API_FML_VERSION = "minecraft/modloader/{0}/";
        private const string API_ADDONS_INFO = "addon/";
        private const string API_ADDON_INFO = "addon/{0}/";
        private const string API_ADDON_DESC = "addon/{0}/description/";
        private const string API_ADDON_FINGERPRINT = "fingerprint/";
        private const string API_ADDON_FILES = "addon/{0}/files/";
        private const string API_ADDON_FILE = "addon/{0}/file/{1}/";
        private const string API_ADDON_FILE_CHANGE = "addon/{0}/file/{1}/changelog/";
        private const string API_ADDON_FILE_DOWNLOAD = "addon/{0}/file/{1}/download-url/";
        
        private readonly HttpClient _client;

        /// <summary>
        /// The default <see cref="Utility.RetryPolicy"/> to use during any communication with the CurseForge API
        /// </summary>
        public static RetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Constructs a new <see cref="ForgeClient"/> instance
        /// </summary>
        public ForgeClient()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };

            RetryPolicy = new RetryPolicy(5, 5000, ex => throw ex);
        }

        #region Addon

        /// <summary>
        /// Retries the information about a addon specified using an id
        /// </summary>
        /// <param name="addonId">The identifier of the addon</param>
        /// <returns>Information about the addon</returns>
        public async Task<CurseJSON.AddonInfo> GetAddonInfoAsync(string addonId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_INFO, addonId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo>();
        }
        /// <summary>
        /// Retries the information about a addon specified using an id
        /// </summary>
        /// <param name="addonId">The identifier of the addon</param>
        /// <returns>Information about the addon</returns>
        public CurseJSON.AddonInfo GetAddonInfo(string addonId)
        {
            return AsyncContext.Run(() => GetAddonInfoAsync(addonId));
        }

        /// <summary>
        /// Retries the information about addons specified using an array of ids
        /// </summary>
        /// <param name="addonIds">The array containg the identifiers</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/></returns>
        public async Task<CurseJSON.AddonInfo[]> GetMultipleAddonInfoAsync(string[] addonIds)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.PostAsync(API_ADDONS_INFO, JsonContent.FromObject(addonIds)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>();
        }
        /// <summary>
        /// Retries the information about addons specified using an array of ids
        /// </summary>
        /// <param name="addonIds">The array containg the identifiers</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/></returns>
        public CurseJSON.AddonInfo[] GetMultipleAddonInfo(string[] addonIds)
        {
            return AsyncContext.Run(() => GetMultipleAddonInfoAsync(addonIds));
        }

        /// <summary>
        /// Retries the HTML description of the specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>HTML description of the addon</returns>
        public async Task<string> GetAddonDescriptionAsync(string addonId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_DESC, addonId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Retries the HTML description of the specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>HTML description of the addon</returns>
        public string GetAddonDescription(string addonId)
        {
            return AsyncContext.Run(() => GetAddonDescriptionAsync(addonId));
        }

        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public async Task<CurseJSON.AddonFile[]> GetAddonFilesAsync(string addonId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_FILES, addonId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync< CurseJSON.AddonFile[]>();
        }
        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public CurseJSON.AddonFile[] GetAddonFiles(string addonId)
        {
            return AsyncContext.Run(() => GetAddonFilesAsync(addonId));
        }

        /// <summary>
        /// Retries the HTML changelog of the specified addon's file
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>HTML changelog of the addon's file</returns>
        public async Task<string> GetAddonFileChangeLogAsync(string addonId, string fileId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_FILE_CHANGE, addonId, fileId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Retries the HTML changelog of the specified addon's file
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>HTML changelog of the addon's file</returns>
        public string GetAddonFileChangeLog(string addonId, string fileId)
        {
            return AsyncContext.Run(() => GetAddonFileChangeLogAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public async Task<CurseJSON.AddonFile> GetAddonFileAsync(string addonId, string fileId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_FILE, addonId, fileId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.AddonFile>();
        }
        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public CurseJSON.AddonFile GetAddonFile(string addonId, string fileId)
        {
            return AsyncContext.Run(() => GetAddonFileAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public async Task<string> GetAddonFileUrlAsync(string addonId, string fileId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_ADDON_FILE_DOWNLOAD, addonId, fileId)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public string GetAddonUrlFile(string addonId, string fileId)
        {
            return AsyncContext.Run(() => GetAddonFileUrlAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>
        public async Task<CurseJSON.PackageFingerprint> GetPackageFingerprintAsync(uint[] fingerprints)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.PostAsync(API_ADDON_FINGERPRINT, JsonContent.FromObject(fingerprints)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.PackageFingerprint>();
        }
        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>
        public CurseJSON.PackageFingerprint GetAddonsFingerprint(uint[] fingerprints)
        {
            return AsyncContext.Run(() => GetPackageFingerprintAsync(fingerprints));
        }

        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <remarks>
        /// Look at the documentation of each query argument
        /// </remarks>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="offset">The amount of mods to skip (e.g: You retrieve 10, but skip 3. You skip the first 3 mods in the list, then retrieve 10</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <param name="category">The category filter of this query (Addons, Server Utility, ...)</param>
        /// <param name="sorting">The method of sorting the addons from which to query</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public async Task<CurseJSON.AddonInfo[]> SearchAddonsAsync(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            AddonCategory category = AddonCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            string url = AddonSearchData.BuildSearchUrl(gameVersion, addonName, offset, amount, category, sorting, kind);
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(url));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>();
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <remarks>
        /// Look at the documentation of each query argument
        /// </remarks>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="offset">The amount of mods to skip (e.g: You retrieve 10, but skip 3. You skip the first 3 mods in the list, then retrieve 10</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <param name="category">The category filter of this query (Addons, Server Utility, ...)</param>
        /// <param name="sorting">The method of sorting the addons from which to query</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public CurseJSON.AddonInfo[] SearchAddons(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            AddonCategory category = AddonCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            return AsyncContext.Run(() => SearchAddonsAsync(addonName, gameVersion, amount, offset, kind, category, sorting));
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <param name="data">Data structure containing the information for querying addons</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public async Task<CurseJSON.AddonInfo[]> SearchAddonsAsync(AddonSearchData data)
        {
            string url = data.BuildSearchUrl();
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(url));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>();
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <param name="data">Data structure containing the information for querying addons</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public CurseJSON.AddonInfo[] SearchAddons(AddonSearchData data)
        {
            return AsyncContext.Run(() => SearchAddonsAsync(data));
        }

        /// <summary>
        /// Constructs a new <see cref="AddonSearchIterator"/> for querying addons from the API
        /// </summary>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="offset">The amount of mods to skip (e.g: You retrieve 10, but skip 3. You skip the first 3 mods in the list, then retrieve 10</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <param name="category">The category filter of this query (Addons, Server Utility, ...)</param>
        /// <param name="sorting">The method of sorting the addons from which to query</param>
        /// <returns>The constructed iterator</returns>
        public AddonSearchIterator CreateAddonIterator(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            AddonCategory category = AddonCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            return new AddonSearchIterator(addonName, gameVersion, amount, offset, kind, category, sorting);
        }

        /// <summary>
        /// Constructs a new <see cref="AddonSearchIterator"/> for querying addons from the API
        /// </summary>
        /// <param name="data">The data for the querying</param>
        /// <returns>The constructed iterator</returns>
        public AddonSearchIterator CreateAddonIterator(AddonSearchData data)
        {
            return new AddonSearchIterator(data);
        }

        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <remarks>
        /// Look at the documentation of each query argument
        /// </remarks>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/></returns>
        public async Task<CurseJSON.AddonInfo[]> SearchAddonsAsync(string addonName, string gameVersion, int amount, AddonKind kind)
        {
            return await SearchAddonsAsync(addonName: addonName, gameVersion: gameVersion, amount: amount, kind: kind);
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <remarks>
        /// Look at the documentation of each query argument
        /// </remarks>
        /// <param name="addonName">The name filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="gameVersion">The game version filter. If <see langword="null"/> or <see cref="String.Empty"/>, ignores this filter option</param>
        /// <param name="amount">The amount of mods to retrieve</param>
        /// <param name="kind">The kind of addon you are querying (Mod, World, ...)</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/></returns>
        public CurseJSON.AddonInfo[] SearchAddons(string addonName, string gameVersion, int amount, AddonKind kind)
        {
            return AsyncContext.Run(() => SearchAddonsAsync(addonName, gameVersion, amount, kind));
        }

        #endregion

        #region Version

        /// <summary>
        /// Retries all the minecraft versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.MinecraftVersionList.versions"/></returns>
        public async Task<CurseJSON.MinecraftVersion[]> GetMinecraftVersionsAsync()
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(API_MC_VERSIONS));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.MinecraftVersionList>().ContinueWith(x => x.Result.versions);
        }
        /// <summary>
        /// Retries all the minecraft versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.MinecraftVersionList.versions"/></returns>
        public CurseJSON.MinecraftVersion[] GetMinecraftVersions()
        {
            return AsyncContext.Run(() => GetMinecraftVersionsAsync());
        }

        /// <summary>
        /// Retrieves information about a specific version of minecraft
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the minecraft version (e.g: '1.12.2')</param>
        /// <returns>The information about the specific version</returns>
        public async Task<CurseJSON.MinecraftVersion> GetMinecraftVersionAsync(string version)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_MC_VERSION, version)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.MinecraftVersion>();
        }
        /// <summary>
        /// Retrieves information about a specific version of minecraft
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the minecraft version (e.g: '1.12.2')</param>
        /// <returns>The information about the specific version</returns>
        public CurseJSON.MinecraftVersion GetMinecraftVersion(string version)
        {
            return AsyncContext.Run(() => GetMinecraftVersionAsync(version));
        }

        /// <summary>
        /// Retries all the Forge Mod Loader versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.ForgeVersionList.versions"/></returns>
        public async Task<CurseJSON.ForgeVersion[]> GetForgeVersionsAsync()
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(API_FML_VERSIONS));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.ForgeVersionList>().ContinueWith(x => x.Result.versions);
        }
        /// <summary>
        /// Retries all the Forge Mod Loader versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.ForgeVersionList.versions"/></returns>
        public CurseJSON.ForgeVersion[] GetForgeVersions()
        {
            return AsyncContext.Run(() => GetForgeVersions());
        }

        /// <summary>
        /// Retrieves information about a specific version of FML
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the FML version (e.g: 'forge-12.17.0.1980')</param>
        /// <returns>The information about the specific version</returns>
        public async Task<CurseJSON.ForgeVersion> GetForgeVersionAsync(string version)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => _client.GetAsync(string.Format(API_FML_VERSION, version)));
            var resp = result.Value.EnsureSuccessStatusCode();

            return await resp.ParseJsonAsync<CurseJSON.ForgeVersion>();
        }
        /// <summary>
        /// Retrieves information about a specific version of FML
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the FML version (e.g: 'forge-12.17.0.1980')</param>
        /// <returns>The information about the specific version</returns>
        public CurseJSON.MinecraftVersion GetForgeVersion(string version)
        {
            return AsyncContext.Run(() => GetMinecraftVersionAsync(version));
        }

        #endregion

        /// <summary>
        /// Releases all allocated resources and disposes all <see cref="IDisposable"/>s instantiated
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
