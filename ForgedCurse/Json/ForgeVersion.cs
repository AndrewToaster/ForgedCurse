using System;
using System.Text.Json.Serialization;

namespace ForgedCurse.Sections
{
    public class ForgeVersion
    {
        [JsonPropertyName("id")]
        public int Identifier { get; set; }

        public int GameVersionId { get; set; }
        public int MinecraftGameVersionId { get; set; }

        /// <summary>
        /// The string representation of the numerical version
        /// </summary>
        [JsonPropertyName("forgeVersion")]
        public string VersionString { get; set; }

        /// <summary>
        /// The string representation of the version
        /// </summary>
        /// <remarks>
        /// This appears to be <see cref="VersionString"/> with the 'forge-' prefix
        /// </remarks>
        public string Name { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// The URL link for the FML file
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// The name of the JAR file
        /// </summary>
        public string FileName { get; set; }

        public int InstallMethod { get; set; }

        /// <summary>
        /// Whether or not this is the latest release
        /// </summary>
        public bool Latest { get; set; }

        /// <summary>
        /// Whether or not this version is recommended (i.e. Is stable and is not in Alpha/Beta stage)
        /// </summary>
        public bool Recommended { get; set; }

        public bool Approved { get; set; }

        /// <summary>
        /// The release date of this FML version
        /// </summary>
        [JsonPropertyName("dateModified")]
        public DateTime ReleaseDate { get; set; }

        public string MavenVersionString { get; set; }

        /// <summary>
        /// String representation of this FML version's JSON file
        /// </summary>
        public string VersionJson { get; set; }

        public string LibrariesInstallLocation { get; set; }
        public string MinecraftVersion { get; set; }
        public object AdditionalFilesJson { get; set; }
        public int ModLoaderGameVersionId { get; set; }
        public int ModLoaderGameVersionTypeId { get; set; }
        public int ModLoaderGameVersionStatus { get; set; }
        public int ModLoaderGameVersionTypeStatus { get; set; }
        public int McGameVersionId { get; set; }
        public int McGameVersionTypeId { get; set; }
        public int McGameVersionStatus { get; set; }
        public int McGameVersionTypeStatus { get; set; }
        public object InstallProfileJson { get; set; }
    }
}