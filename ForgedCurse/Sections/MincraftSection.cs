using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Json;

namespace ForgedCurse.Sections
{
    /// <summary>
    /// A section class containing methods to retrieve information for the game Minecraft
    /// </summary>
    public sealed class MincraftSection : BaseSection
    {
        private const string API_FML_VERSIONS = "minecraft/modloader/";
        private const string API_FML_VERSION = "minecraft/modloader/{0}/";
        private const string API_VERSION = "minecraft/version/";

        internal MincraftSection(ForgeClient client) : base(client)
        {
        }

        public Task<ForgeVersionLite[]> RetrieveModLoaderVersions()
        {
            return HttpGetJson<ForgeVersionLite[]>(API_FML_VERSIONS);
        }

        public Task<ForgeVersion> RetrieveModLoaderVersion(string version)
        {
            return HttpGetJson<ForgeVersion>(string.Format(API_FML_VERSION, version));
        }

        public Task<MinecraftVersion[]> RetrieveGameVersions()
        {
            return HttpGetJson<MinecraftVersion[]>(API_VERSION);
        }
    }
}
