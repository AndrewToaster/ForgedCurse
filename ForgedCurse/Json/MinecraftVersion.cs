using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class MinecraftVersion
    {
        [JsonPropertyName("id")]
        public int Identifier { get; set; }

        public int GameVersionId { get; set; }

        /// <summary>
        /// The string representation of the version
        /// </summary>
        public string VersionString { get; set; }

        /// <summary>
        /// The download URL for the version's JAR file
        /// </summary>
        [JsonPropertyName("jarDownloadUrl")]
        public string JarUrl { get; set; }

        /// <summary>
        /// The download URL for the version's JSON file
        /// </summary>
        [JsonPropertyName("jsonDownloadUrl")]
        public string JsonUrl { get; set; }

        /// <summary>
        /// No idea
        /// </summary>
        /// <remarks>
        /// This value appears to be always false
        /// </remarks>
        public bool Approved { get; set; }


        /// <summary>
        /// The release date of this version
        /// </summary>
        [JsonPropertyName("dateModified")]
        public DateTime ReleaseDate { get; set; }

        public int GameVersionTypeId { get; set; }
        public int GameVersionStatus { get; set; }
        public int GameVersionTypeStatus { get; set; }
    }
}
