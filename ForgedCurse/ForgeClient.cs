using ForgedCurse.Enumeration;
using ForgedCurse.Utility;
using ForgedCurse.WrapperTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForgedCurse
{
    /// <summary>
    /// Main class for interacting with the CurseForge API
    /// </summary>
    /// <remarks>
    /// Documentation taken from 'https://twitchappapi.docs.apiary.io/'
    /// </remarks>
    public sealed class ForgeClient : IDisposable
    {
        private const string API_URL = "https://addons-ecs.forgesvc.net/api/v2/";
        private const string API_MC_VERSIONS = "minecraft/version/";
        private const string API_MC_VERSION = "minecraft/version/{0}/";
        private const string API_FML_VERSIONS = "minecraft/modloader/";
        private const string API_FML_VERSION = "minecraft/modloader/{0}/";
        private const string API_ADDON_FILE_CHANGE = "addon/{0}/file/{1}/changelog/";
        private const string API_ADDON_FILE_DOWNLOAD = "addon/{0}/file/{1}/download-url/";

        public readonly HttpClient HttpClient;

        public AddonSection Addons { get; }
        public FileSection Files { get; }

        /// <summary>
        /// The default <see cref="Utility.RetryPolicy"/> to use during any communication with the CurseForge API
        /// </summary>
        public static RetryPolicy RetryPolicy { get; set; }

        internal static JsonSerializerOptions SerializerSettings { get; }

        static ForgeClient()
        {
            RetryPolicy = new(5, 5000, ex => throw ex);
            SerializerSettings = new()
            {
            };
        }

        /// <summary>
        /// Constructs a new <see cref="ForgeClient"/> instance
        /// </summary>
        public ForgeClient()
        {
            HttpClient = new()
            {
                BaseAddress = new Uri(API_URL)
            };
            Addons = new(this);
            Files = new(this);
        }

        #region Addon

        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public async Task<IEnumerable<AddonFile>> GetAddonFilesAsync(string addonId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_ADDON_FILES, addonId)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.ParseJsonAsync<CurseJSON.AddonFile[]>().ContinueWith(task => task.Result.Select(x => new AddonFile(x, this)));
        }
        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public IEnumerable<AddonFile> GetAddonFiles(string addonId)
        {
            return AsyncContext.Run(() => GetAddonFilesAsync(addonId));
        }

        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public Task<IEnumerable<AddonFile>> GetAddonFilesAsync(int addonId)
        {
            return GetAddonFilesAsync(addonId.ToString());
        }
        /// <summary>
        /// Retries the files of a specified addon
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <returns>Information about the addon's files</returns>
        public IEnumerable<AddonFile> GetAddonFiles(int addonId)
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
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_ADDON_FILE_CHANGE, addonId, fileId)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
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
        /// Retries the HTML changelog of the specified addon's file
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>HTML changelog of the addon's file</returns>
        public Task<string> GetAddonFileChangeLogAsync(int addonId, int fileId)
        {
            return GetAddonFileChangeLogAsync(addonId.ToString(), fileId.ToString());
        }
        /// <summary>
        /// Retries the HTML changelog of the specified addon's file
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>HTML changelog of the addon's file</returns>
        public string GetAddonFileChangeLog(int addonId, int fileId)
        {
            return AsyncContext.Run(() => GetAddonFileChangeLogAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public async Task<AddonFile> GetAddonFileAsync(string addonId, string fileId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_ADDON_FILE, addonId, fileId)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return new AddonFile(await resp.ParseJsonAsync<CurseJSON.AddonFile>().ConfigureAwait(false), this);
        }
        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public AddonFile GetAddonFile(string addonId, string fileId)
        {
            return AsyncContext.Run(() => GetAddonFileAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public Task<AddonFile> GetAddonFileAsync(int addonId, int fileId)
        {
            return GetAddonFileAsync(addonId.ToString(), fileId.ToString());
        }
        /// <summary>
        /// Retries the addon's file information
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The information of the addon's file</returns>
        public AddonFile GetAddonFile(int addonId, int fileId)
        {
            return AsyncContext.Run(() => GetAddonFileAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public async Task<string> GetAddonFileDownloadAsync(string addonId, string fileId)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_ADDON_FILE_DOWNLOAD, addonId, fileId)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public string GetAddonFileDownload(string addonId, string fileId)
        {
            return AsyncContext.Run(() => GetAddonFileDownloadAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public Task<string> GetAddonFileDownloadAsync(int addonId, int fileId)
        {
            return GetAddonFileDownloadAsync(addonId.ToString(), fileId.ToString());
        }
        /// <summary>
        /// Retries the addon's file download url
        /// </summary>
        /// <param name="addonId">The identification of the addon</param>
        /// <param name="fileId">The identification of the addon's file</param>
        /// <returns>The URL for the download</returns>
        public string GetAddonFileDownload(int addonId, int fileId)
        {
            return AsyncContext.Run(() => GetAddonFileDownloadAsync(addonId, fileId));
        }

        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>
        public Task<PackageFingerprint> GetPackageFingerprintAsync(params uint[] fingerprints)
        {
            return GetPackageFingerprintAsync(fingerprints.Cast<long>().ToArray());
        }
        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>
        public PackageFingerprint GetPackageFingerprint(params uint[] fingerprints)
        {
            return AsyncContext.Run(() => GetPackageFingerprintAsync(fingerprints));
        }
        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>

        public async Task<PackageFingerprint> GetPackageFingerprintAsync(params long[] fingerprints)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.PostAsync(API_ADDON_FINGERPRINT, JsonContent.FromObject(fingerprints)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return new PackageFingerprint(await resp.ParseJsonAsync<CurseJSON.PackageFingerprint>().ConfigureAwait(false), this);
        }
        /// <summary>
        /// Retries addons' package information based on fingerprints
        /// </summary>
        /// <param name="fingerprints">Array of the addons' package fingerprints</param>
        /// <returns>The information of the addons' package fingerprints</returns>
        public PackageFingerprint GetPackageFingerprint(params long[] fingerprints)
        {
            return AsyncContext.Run(() => GetPackageFingerprintAsync(fingerprints));
        }

        /// <summary>
        /// Get a <see cref="Addon"/> from a computated fingerprint
        /// </summary>
        /// <param name="fingerprint">The fingerprint to use</param>
        /// <returns>The <see cref="Addon"/> retrieved</returns>
        public async Task<Addon> GetAddonFromFingerprintAsync(long fingerprint)
        {
            var fpData = await GetPackageFingerprintAsync(fingerprint).ConfigureAwait(false);

            if (fpData.Matches.Count == 0 || fpData.UnmatchedFingerprints.Contains(fingerprint))
            {
                return null;
            }

            var fpMatch = fpData.Matches.First();
            var addon = await GetAddonAsync(fpMatch.ProjectIdentifier).ConfigureAwait(false);

            return new Addon(addon, this);
        }
        /// <summary>
        /// Get a <see cref="Addon"/> from a computated fingerprint
        /// </summary>
        /// <param name="fingerprint">The fingerprint to use</param>
        /// <returns>The <see cref="Addon"/> retrieved</returns>
        public Addon GetAddonFromFingerprint(long fingerprint)
        {
            return AsyncContext.Run(() => GetAddonFromFingerprintAsync(fingerprint));
        }

        /// <summary>
        /// Get a <see cref="Addon"/> from a computated fingerprint from <paramref name="jarFile"/>
        /// </summary>
        /// <remarks>
        /// Internally calls the <see cref="Fingerprinting.ComputeFingerprint(string)"/> supplied with <paramref name="jarFile"/>, then retrieves the the file from the fingerprint
        /// </remarks>
        /// <param name="jarFile">The path to the JAR file of the addon</param>
        /// <returns>The <see cref="Addon"/> retrieved</returns>
        public Task<Addon> GetAddonFromFileAsync(string jarFile)
        {
            long fp = Fingerprinting.ComputeFingerprint(jarFile);
            return GetAddonFromFingerprintAsync(fp);
        }
        /// <summary>
        /// Get a <see cref="Addon"/> from a computated fingerprint from <paramref name="jarFile"/>
        /// </summary>
        /// <remarks>
        /// Internally calls the <see cref="Fingerprinting.ComputeFingerprint(string)"/> supplied with <paramref name="jarFile"/>, then retrieves the the file from the fingerprint
        /// </remarks>
        /// <param name="jarFile">The path to the JAR file of the addon</param>
        /// <returns>The <see cref="Addon"/> retrieved</returns>
        public Addon GetAddonFromFile(string jarFile)
        {
            return AsyncContext.Run(() => GetAddonFromFileAsync(jarFile));
        }

        /// <summary>
        /// Get a <see cref="AddonFile"/> from a computated fingerprint
        /// </summary>
        /// <param name="fingerprint">The fingerprint to use</param>
        /// <returns>The <see cref="AddonFile"/> retrieved</returns>
        public async Task<AddonFile> GetAddonFileFromFingerprintAsync(long fingerprint)
        {
            var fpData = await GetPackageFingerprintAsync(fingerprint).ConfigureAwait(false);

            if (fpData.Matches.Count == 0 || fpData.UnmatchedFingerprints.Contains(fingerprint))
            {
                return null;
            }

            var fpMatch = fpData.Matches.First();
            var file = await GetAddonFileAsync(fpMatch.ProjectIdentifier, fpMatch.FileIdentifier).ConfigureAwait(false);

            return new AddonFile(file, this);
        }
        /// <summary>
        /// Get a <see cref="AddonFile"/> from a computated fingerprint
        /// </summary>
        /// <param name="fingerprint">The fingerprint to use</param>
        /// <returns>The <see cref="AddonFile"/> retrieved</returns>
        public AddonFile GetAddonFileFromFingerprint(long fingerprint)
        {
            return AsyncContext.Run(() => GetAddonFileFromFingerprintAsync(fingerprint));
        }

        /// <summary>
        /// Get a <see cref="AddonFile"/> from a computated fingerprint from <paramref name="jarFile"/>
        /// </summary>
        /// <remarks>
        /// Internally calls the <see cref="Fingerprinting.ComputeFingerprint(string)"/> supplied with <paramref name="jarFile"/>, then retrieves the the file from the fingerprint
        /// </remarks>
        /// <param name="jarFile">The path to the JAR file of the addon</param>
        /// <returns>The <see cref="AddonFile"/> retrieved</returns>
        public Task<AddonFile> GetAddonFileFromFileAsync(string jarFile)
        {
            long fp = Fingerprinting.ComputeFingerprint(jarFile);
            return GetAddonFileFromFingerprintAsync(fp);
        }
        /// <summary>
        /// Get a <see cref="AddonFile"/> from a computated fingerprint from <paramref name="jarFile"/>
        /// </summary>
        /// <remarks>
        /// Internally calls the <see cref="Fingerprinting.ComputeFingerprint(string)"/> supplied with <paramref name="jarFile"/>, then retrieves the the file from the fingerprint
        /// </remarks>
        /// <param name="jarFile">The path to the JAR file of the addon</param>
        /// <returns>The <see cref="AddonFile"/> retrieved</returns>
        public AddonFile GetAddonFileFromFile(string jarFile)
        {
            return AsyncContext.Run(() => GetAddonFileFromFile(jarFile));
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
        public async Task<IEnumerable<Addon>> SearchAddonsAsync(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            MinecraftCategory category = MinecraftCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            string url = AddonSearchData.BuildSearchUrl(gameVersion, addonName, amount, offset, category, sorting, kind);
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(url).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>().ContinueWith(task => task.Result.Select(x => new Addon(x, this)));
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
        public IEnumerable<Addon> SearchAddons(string addonName = "", string gameVersion = "", int amount = 10, int offset = 0, AddonKind kind = AddonKind.Mod,
            MinecraftCategory category = MinecraftCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            return AsyncContext.Run(() => SearchAddonsAsync(addonName, gameVersion, amount, offset, kind, category, sorting));
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <param name="data">Data structure containing the information for querying addons</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public async Task<IEnumerable<Addon>> SearchAddonsAsync(AddonSearchData data)
        {
            string url = data.BuildSearchUrl();
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(url).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.ParseJsonAsync<CurseJSON.AddonInfo[]>().ContinueWith(task => task.Result.Select(x => new Addon(x, this))).ConfigureAwait(false);
        }
        /// <summary>
        /// Queries addons using the specified options
        /// </summary>
        /// <param name="data">Data structure containing the information for querying addons</param>
        /// <returns>Retrieved <see cref="CurseJSON.AddonInfo"/>[]</returns>
        public IEnumerable<Addon> SearchAddons(AddonSearchData data)
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
            MinecraftCategory category = MinecraftCategory.All, AddonSorting sorting = AddonSorting.Featured)
        {
            return new AddonSearchIterator(this, addonName, gameVersion, amount, offset, kind, category, sorting);
        }

        /// <summary>
        /// Constructs a new <see cref="AddonSearchIterator"/> for querying addons from the API
        /// </summary>
        /// <param name="data">The data for the querying</param>
        /// <returns>The constructed iterator</returns>
        public AddonSearchIterator CreateAddonIterator(AddonSearchData data)
        {
            return new AddonSearchIterator(this, data);
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
        public Task<IEnumerable<Addon>> SearchAddonsAsync(string addonName, string gameVersion, int amount, AddonKind kind)
        {
            return SearchAddonsAsync(addonName: addonName, gameVersion: gameVersion, amount: amount, kind: kind);
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
        public IEnumerable<Addon> SearchAddons(string addonName, string gameVersion, int amount, AddonKind kind)
        {
            return AsyncContext.Run(() => SearchAddonsAsync(addonName, gameVersion, amount, kind));
        }

        #endregion

        #region Version

        /// <summary>
        /// Retries all the minecraft versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.MinecraftVersionList.versions"/></returns>
        public async Task<IEnumerable<MinecraftVersion>> GetMinecraftVersionsAsync()
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(API_MC_VERSIONS).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.ParseJsonAsync<CurseJSON.MinecraftVersionList>().ContinueWith(task => task.Result.versions.Select(x => new MinecraftVersion(x, this)));
        }
        /// <summary>
        /// Retries all the minecraft versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.MinecraftVersionList.versions"/></returns>
        public IEnumerable<MinecraftVersion> GetMinecraftVersions()
        {
            return AsyncContext.Run(() => GetMinecraftVersionsAsync());
        }

        /// <summary>
        /// Retrieves information about a specific version of minecraft
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the minecraft version (e.g: '1.12.2')</param>
        /// <returns>The information about the specific version</returns>
        public async Task<MinecraftVersion> GetMinecraftVersionAsync(string version)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_MC_VERSION, version)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return new MinecraftVersion(await resp.ParseJsonAsync<CurseJSON.MinecraftVersion>().ConfigureAwait(false), this);
        }
        /// <summary>
        /// Retrieves information about a specific version of minecraft
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the minecraft version (e.g: '1.12.2')</param>
        /// <returns>The information about the specific version</returns>
        public MinecraftVersion GetMinecraftVersion(string version)
        {
            return AsyncContext.Run(() => GetMinecraftVersionAsync(version));
        }

        /// <summary>
        /// Retries all the Forge Mod Loader versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.ForgeVersionList.versions"/></returns>
        public async Task<IEnumerable<ForgeVersion>> GetForgeVersionsAsync()
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(API_FML_VERSIONS).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return await resp.ParseJsonAsync<CurseJSON.ForgeVersion[]>().ContinueWith(t => t.Result.Select(x => new ForgeVersion(x, this))).ConfigureAwait(false);
        }
        /// <summary>
        /// Retries all the Forge Mod Loader versions
        /// </summary>
        /// <returns>Retrieved <see cref="CurseJSON.ForgeVersionList.versions"/></returns>
        public IEnumerable<ForgeVersion> GetForgeVersions()
        {
            return AsyncContext.Run(() => GetForgeVersionsAsync());
        }

        /// <summary>
        /// Retrieves information about a specific version of FML
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the FML version (e.g: 'forge-12.17.0.1980')</param>
        /// <returns>The information about the specific version</returns>
        public async Task<ForgeVersion> GetForgeVersionAsync(string version)
        {
            var result = await RetryPolicy.ExecutePolicyAsync(() => HttpClient.GetAsync(string.Format(API_FML_VERSION, version)).CheckSuccess()).ConfigureAwait(false);
            var resp = result.Value;

            return new ForgeVersion(await resp.ParseJsonAsync<CurseJSON.ForgeVersion>().ConfigureAwait(false), this);
        }
        /// <summary>
        /// Retrieves information about a specific version of FML
        /// </summary>
        /// <param name="version">The <see cref="string"/> representation of the FML version (e.g: 'forge-12.17.0.1980')</param>
        /// <returns>The information about the specific version</returns>
        public MinecraftVersion GetForgeVersion(string version)
        {
            return AsyncContext.Run(() => GetMinecraftVersionAsync(version));
        }

        #endregion

        /// <summary>
        /// Releases all allocated resources and disposes all <see cref="IDisposable"/>s instantiated
        /// </summary>
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
