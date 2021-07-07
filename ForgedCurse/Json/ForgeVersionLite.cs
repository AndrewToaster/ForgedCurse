using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class ForgeVersionLite
    {
        /// <summary>
        /// The name of this version
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The game version this FML version is associated with
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Whether or not this is the newest FML version
        /// </summary>
        public bool Latest { get; set; }

        /// <summary>
        /// Whether or not this FML version is recommended (i.e. Stability or Alpha/Beta release)
        /// </summary>
        public bool Recommended { get; set; }

        /// <summary>
        /// The date this FML version was released
        /// </summary>
        [JsonPropertyName("dateModified")]
        public DateTime ReleaseDate { get; set; }
    }
}
