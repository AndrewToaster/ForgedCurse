using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;
using ForgedCurse.WrapperTypes;

namespace ForgedCurse.Json
{
    public class AddonRelease
    {
        /// <summary>
        /// The name of the addons JAR file
        /// </summary>
        /// <remarks>
        /// Should be same as <see cref="Name"/>
        /// </remarks>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// The name of the file displayed on Curseforge
        /// </summary>
        /// <remarks>
        /// Should be same as <see cref="FileName"/>
        /// </remarks>
        [JsonPropertyName("displayName")]
        public string Name { get; set; }

        /// <summary>
        /// The identifier for this addon file
        /// </summary>
        [JsonPropertyName("id")]
        public int Identifier { get; set; }

        /// <summary>
        /// The identifier of the addon that owns this file
        /// </summary>
        [JsonPropertyName("projectId")]
        public int AddonIdentifier { get; set; }

        /// <summary>
        /// The <see cref="Enumeration.ReleaseType"/> for this file (Alpha, Beta, Release)
        /// </summary>
        /// <remarks>
        /// This value is casted from the <see cref="int"/> value <see cref="CurseJSON.AddonFile.releaseType"/>
        /// </remarks>
        public ReleaseType ReleaseType { get; set; }

        [JsonPropertyName("fileStatus")]
        public ItemStatus Status { get; set; }

        /// <summary>
        /// The date at which this file was released
        /// </summary>
        [JsonPropertyName("fileDate")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The HTML representation of the changelog
        /// </summary>
        public string Changelog { get; set; }

        /// <summary>
        /// The URL for the file download
        /// </summary>
        /// <remarks>
        /// Due to the migration from Twitch to CurseForge, this can point to either Twitch (edge.forgecdn.net) or Overwolf (edge-service.overwolf.wtf) CDN
        /// </remarks>
        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Array of all supported versions for this addon file
        /// </summary>
        [JsonPropertyName("gameVersion")]
        public string[] Versions { get; set; }

        /// <summary>
        /// The hash for this file
        /// </summary>
        /// <remarks>
        /// These hashes are calculated using MurmurHash2 with a seed of 1 and a normalized (removed whitespace characters) array of the file stream (JAR file)
        /// </remarks>
        [JsonPropertyName("packageFingerprint")]
        public uint Hash { get; set; }

        /// <summary>
        /// Boolean value representing whether or not the file is available
        /// </summary>
        /// <remarks>
        /// Files are unavailable if they are either deprecated or the addon is not available
        /// </remarks>
        [JsonPropertyName("isAvailable")]
        public bool Available { get; set; }
    }
}
