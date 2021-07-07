using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class SortableVersion
    {
        /// <summary>
        /// The 0-padded string of the version (e.g. '0000000001.0000000008' for MC 1.8)
        /// </summary>
        [JsonPropertyName("gameVersionPadded")]
        public string Padded { get; set; }

        /// <summary>
        /// The string representation of the version
        /// </summary>
        [JsonPropertyName("gameVersion")]
        public string Version { get; set; }

        /// <summary>
        /// The date this version was released
        /// </summary>
        [JsonPropertyName("gameVersionReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The human read-able name of the version
        /// </summary>
        [JsonPropertyName("gameVersionName")]
        public string Name { get; set; }
    }
}
