using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    public class Module
    {
        /// <summary>
        /// The name of the file in the addon
        /// </summary>
        [JsonPropertyName("foldername")]
        public string Name { get; set; }

        /// <summary>
        /// The hash signature of the module
        /// </summary>
        [JsonPropertyName("fingerprint")]
        public uint Hash { get; set; }
    }
}
