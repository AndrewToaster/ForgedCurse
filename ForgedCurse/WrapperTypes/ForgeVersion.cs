using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgedCurse.Json
{
    /// <summary>
    /// Json-Parsed class containing info about a mod-loader version
    /// </summary>
    public class ForgeVersion
    {
        /// <summary>
        /// The name of this FML version (e.g. 'forge-14.23.5.2838')
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of Minecraft associated with this FML version
        /// </summary>
        [JsonPropertyName("gameVersion")]
        public string MinecraftVersion { get; set; }

        /// <summary>
        /// The time where this FML version was last modified / updated
        /// </summary>
        /// <remarks>
        /// This is probably the date this FML version was released
        /// </remarks>
        [JsonPropertyName("dateModified")]
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Whether or not this FML version has been tested and proved as stable
        /// </summary>
        public bool Recommended { get; set; }

        /// <summary>
        /// Whether or not this FML version is the newest version released for this <see cref="MinecraftVersion"/>
        /// </summary>
        public bool Latest { get; set; }

        /*
        /// <summary>
        /// Returns the <see cref="WrapperTypes.MinecraftVersion"/> that this <see cref="ForgeVersion"/> depends on
        /// </summary>
        /// <returns><see cref="WrapperTypes.MinecraftVersion"/> where its <see cref="MinecraftVersion.Version"/> is equal to <see cref="MinecraftVersion"/></returns>
        public async Task<MinecraftVersion> GetMinecraftVersionAsync()
        {
            return await Client.GetMinecraftVersionAsync(MinecraftVersion);
        }

        /// <summary>
        /// Returns the <see cref="WrapperTypes.MinecraftVersion"/> that this <see cref="ForgeVersion"/> depends on
        /// </summary>
        /// <returns><see cref="WrapperTypes.MinecraftVersion"/> where its <see cref="MinecraftVersion.Version"/> is equal to <see cref="MinecraftVersion"/></returns>
        public MinecraftVersion GetMinecraftVersion()
        {
            return Client.GetMinecraftVersion(MinecraftVersion);
        }
        */
    }
}
