using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgedCurse
{
    /// <summary>
    /// Wrapper around <see cref="JsonConvert"/> with extra functionality. Contains all JSON classes
    /// </summary>
    public static class CurseJSON
    {
        private static readonly JsonSerializerSettings _setting;

        /// <summary>
        /// Initializes the <see cref="_setting"/> value
        /// </summary>
        static CurseJSON()
        {
            _setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// Deserializes (converts from string to obj) JSON into a generic <see cref="object"/> <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type which to convert</typeparam>
        /// <param name="json">The JSON to deserialize</param>
        /// <returns>Deserialized object <typeparamref name="T"/></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _setting);
        }

        /// <summary>
        /// Serializes (converts obj to string) a generic <see cref="object"/> <typeparamref name="T"/> into its JSON representation
        /// </summary>
        /// <typeparam name="T">The type which to convert from</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>Serialized JSON representation of <paramref name="obj"/></returns>
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, _setting);
        }

        #region Json Classes
        /*public class ForgeVersionList
        {
            public ForgeVersion[] versions;

            public static implicit operator ForgeVersion[](ForgeVersionList versionList)
            {
                return versionList.versions;
            }
        }*/

        public class ForgeVersion
        {
            public string name { get; set; }
            public string gameVersion { get; set; }
            public bool latest { get; set; }
            public bool recommended { get; set; }
            public DateTime dateModified { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        public class MinecraftVersionList
        {
            public MinecraftVersion[] versions;

            public static implicit operator MinecraftVersion[](MinecraftVersionList versionList)
            {
                return versionList.versions;
            }
        }

        public class MinecraftVersion
        {
            public int id { get; set; }
            public int gameVersionId { get; set; }
            public string versionString { get; set; }
            public string jarDownloadUrl { get; set; }
            public string jsonDownloadUrl { get; set; }
            public bool approved { get; set; }
            public DateTime dateModified { get; set; }
            public int gameVersionTypeId { get; set; }
            public int gameVersionStatus { get; set; }
            public int gameVersionTypeStatus { get; set; }

            public override string ToString()
            {
                return versionString;
            }
        }

        public class AddonInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public AddonAuthor[] authors { get; set; }
            public AddonAttachment[] attachments { get; set; }
            public string websiteUrl { get; set; }
            public int gameId { get; set; }
            public string summary { get; set; }
            public int defaultFileId { get; set; }
            public float downloadCount { get; set; }
            public AddonFile[] latestFiles { get; set; }
            public Category[] categories { get; set; }
            public int status { get; set; }
            public int primaryCategoryId { get; set; }
            public AddonCategorySection categorySection { get; set; }
            public string slug { get; set; }
            public GameVersionLatestFile[] gameVersionLatestFiles { get; set; }
            public bool isFeatured { get; set; }
            public float popularityScore { get; set; }
            public int gamePopularityRank { get; set; }
            public string primaryLanguage { get; set; }
            public string gameSlug { get; set; }
            public string gameName { get; set; }
            public string portalName { get; set; }
            public DateTime dateModified { get; set; }
            public DateTime dateCreated { get; set; }
            public DateTime dateReleased { get; set; }
            public bool isAvailable { get; set; }
            public bool isExperiemental { get; set; }
        }

        public class AddonCategorySection
        {
            public int id { get; set; }
            public int gameId { get; set; }
            public string name { get; set; }
            public int packageType { get; set; }
            public string path { get; set; }
            public string initialInclusionPattern { get; set; }
            public object extraIncludePattern { get; set; }
            public int gameCategoryId { get; set; }
        }

        public class AddonAuthor
        {
            public string name { get; set; }
            public string url { get; set; }
            public int projectId { get; set; }
            public int id { get; set; }
            public object projectTitleId { get; set; }
            public object projectTitleTitle { get; set; }
            public int userId { get; set; }
            public int twitchId { get; set; }
        }

        public class AddonAttachment
        {
            public int id { get; set; }
            public int projectId { get; set; }
            public string description { get; set; }
            public bool isDefault { get; set; }
            public string thumbnailUrl { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public int status { get; set; }
        }

        public class AddonFile
        {
            public int id { get; set; }
            public string displayName { get; set; }
            public string fileName { get; set; }
            public DateTime fileDate { get; set; }
            public int fileLength { get; set; }
            public int releaseType { get; set; }
            public int fileStatus { get; set; }
            public string downloadUrl { get; set; }
            public bool isAlternate { get; set; }
            public int alternateFileId { get; set; }
            public Dependency[] dependencies { get; set; }
            public bool isAvailable { get; set; }
            public Module[] modules { get; set; }
            public long packageFingerprint { get; set; }
            public string[] gameVersion { get; set; }
            public SortableGameVersion[] sortableGameVersion { get; set; }
            public object installMetadata { get; set; }
            public string changelog { get; set; }
            public bool hasInstallScript { get; set; }
            public bool isCompatibleWithClient { get; set; }
            public int categorySectionPackageType { get; set; }
            public int restrictProjectFileAccess { get; set; }
            public int projectStatus { get; set; }
            public int renderCacheId { get; set; }
            public object fileLegacyMappingId { get; set; }
            public int projectId { get; set; }
            public object parentProjectFileId { get; set; }
            public object parentFileLegacyMappingId { get; set; }
            public object fileTypeId { get; set; }
            public object exposeAsAlternative { get; set; }
            public int packageFingerprintId { get; set; }
            public DateTime gameVersionDateReleased { get; set; }
            public int gameVersionMappingId { get; set; }
            public int gameVersionId { get; set; }
            public int gameId { get; set; }
            public bool isServerPack { get; set; }
            public object serverPackFileId { get; set; }
            public object gameVersionFlavor { get; set; }
        }

        public class Module
        {
            public string foldername { get; set; }
            public long fingerprint { get; set; }
        }


        public class Dependency
        {
            public int id { get; set; }
            public int addonId { get; set; }
            public int type { get; set; }
            public int fileId { get; set; }
        }

        public class SortableGameVersion
        {
            public string gameVersionPadded { get; set; }
            public string gameVersion { get; set; }
            public DateTime gameVersionReleaseDate { get; set; }
            public string gameVersionName { get; set; }
        }

        public class Category
        {
            public int categoryId { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string avatarUrl { get; set; }
            public int parentId { get; set; }
            public int rootId { get; set; }
            public int projectId { get; set; }
            public int avatarId { get; set; }
            public int gameId { get; set; }
        }

        public class GameVersionLatestFile
        {
            public string gameVersion { get; set; }
            public int projectFileId { get; set; }
            public string projectFileName { get; set; }
            public int fileType { get; set; }
            public object gameVersionFlavor { get; set; }
        }

        public class PackageFingerprint
        {
            public bool isCacheBuilt { get; set; }
            public ExactMatch[] exactMatches { get; set; }
            public long[] exactFingerprints { get; set; }
            public long[] partialMatches { get; set; }
            public PartialMatchFingerprints partialMatchFingerprints { get; set; }
            public long[] installedFingerprints { get; set; }
            public long[] unmatchedFingerprints { get; set; }
        }

        public class PartialMatchFingerprints
        {
        }

        public class ExactMatch
        {
            public int id { get; set; }
            public AddonFile file { get; set; }
            public AddonFile[] latestFiles { get; set; }
        }

        #endregion
    }
}
