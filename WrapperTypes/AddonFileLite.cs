using ForgedCurse.Enumeration;
using System;
using System.Threading.Tasks;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.GameVersionLatestFile"/> class
    /// </summary>
    /// <remarks>
    /// Difference between <see cref="AddonFile"/> and <see cref="AddonFileLite"/> is that <see cref="AddonFileLite"/> has only the most necessary information (e.g. Id, Version, Type)
    /// </remarks>
    public class AddonFileLite : ForgeWrapper<CurseJSON.GameVersionLatestFile>
    {
        public AddonFileLite(CurseJSON.GameVersionLatestFile gameVersionFile, ForgeClient client) : base(gameVersionFile, client)
        {
        }

        /// <summary>
        /// The version this package is for
        /// </summary>
        public string Version { get => WrappedType.gameVersion; }

        /// <summary>
        /// The identifier for this file
        /// </summary>
        public int Identifier { get => WrappedType.projectFileId; }

        /// <summary>
        /// The name of this addon file (e.g. 'jei_1.12.2-4.16.1.302.jar')
        /// </summary>
        public string FileName { get => WrappedType.projectFileName; }

        /// <summary>
        /// The <see cref="Enumeration.ReleaseType"/> of this addon file (Alpha, Beta, Release)
        /// </summary>
        /// <remarks>
        /// This value is casted from the <see cref="int"/> value <see cref="CurseJSON.GameVersionLatestFile.fileType"/>
        /// </remarks>
        public ReleaseType ReleaseType { get => (ReleaseType)WrappedType.fileType; }

        /// <summary>
        /// Retrieves <see cref="AddonFile"/> from the specified addon
        /// </summary>
        /// <remarks>
        /// Due to CurseForge not providing the <see cref="Addon.Identifier"/>, you need to specify one
        /// </remarks>
        /// <param name="addonId">The id of the addon</param>
        /// <returns>The retrieved <see cref="AddonFile"/></returns>
        public async Task<AddonFile> GetAddonFileAsync(int addonId)
        {
            var file = await Client.GetAddonFileAsync(addonId, Identifier);
            return new AddonFile(file, Client);
        }
        /// <summary>
        /// Retrieves <see cref="AddonFile"/> from the specified addon
        /// </summary>
        /// <remarks>
        /// Due to CurseForge not providing the <see cref="Addon.Identifier"/>, you need to specify one
        /// </remarks>
        /// <param name="addonId">The id of the addon</param>
        /// <returns>The retrieved <see cref="AddonFile"/></returns>
        public AddonFile GetAddonFile(int addonId)
        {
            var file = Client.GetAddonFile(addonId, Identifier);
            return new AddonFile(file, Client);
        }
    }
}