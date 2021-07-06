using ForgedCurse.Enumeration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.AddonFile"/> class
    /// </summary>
    /// <remarks>
    /// Difference between <see cref="AddonFile"/> and <see cref="AddonFileLite"/> 
    /// is that <see cref="AddonFile"/> has a lot of information about itself(e.g. Fingerprint, Changelog, Download Url)
    /// </remarks>
    public class AddonFile : ForgeWrapper<CurseJSON.AddonFile>
    {
        public AddonFile(CurseJSON.AddonFile addonFile, ForgeClient client) : base(addonFile, client)
        {
        }

        /// <summary>
        /// The name of the addons JAR file
        /// </summary>
        /// <remarks>
        /// From my testing <see cref="CurseJSON.AddonFile.fileName"/> is the same as <see cref="CurseJSON.AddonFile.displayName"/>
        /// </remarks>
        public string FileName { get => WrappedType.fileName; }

        /// <summary>
        /// The identifier for this addon file
        /// </summary>
        public int Identifier { get => WrappedType.id; }

        /// <summary>
        /// The identifier of the addon that owns this file
        /// </summary>
        public int ProjectIdentifier { get => WrappedType.projectId; }

        /// <summary>
        /// The <see cref="Enumeration.ReleaseType"/> for this file (Alpha, Beta, Release)
        /// </summary>
        /// <remarks>
        /// This value is casted from the <see cref="int"/> value <see cref="CurseJSON.AddonFile.releaseType"/>
        /// </remarks>
        public ReleaseType ReleaseType { get => (ReleaseType)WrappedType.releaseType; }

        /// <summary>
        /// The date at which this file was released
        /// </summary>
        public DateTime ReleaseDate { get => WrappedType.fileDate; }

        /// <summary>
        /// The HTML representation of the changelog
        /// </summary>
        public string Changelog { get => WrappedType.changelog; }

        /// <summary>
        /// The URL for the file download
        /// </summary>
        /// <remarks>
        /// Due to the migration from Twitch to CurseForge, this can point to either Twitch (edge.forgecdn.net) or Overwolf (edge-service.overwolf.wtf) CDN
        /// </remarks>
        public string DownloadUrl { get => WrappedType.downloadUrl; }

        /// <summary>
        /// Array of all supported versions for this addon file
        /// </summary>
        public string[] Versions { get => WrappedType.gameVersion; }

        /// <summary>
        /// The fingerprint for this file
        /// </summary>
        /// <remarks>
        /// Fingerprints are MurmurHash2 hashes. Using a seed of 1 and a normalized (removed whitespace characters) array of the file stream (JAR file)
        /// </remarks>
        public long Fingerprint { get => WrappedType.packageFingerprint; }

        /// <summary>
        /// Boolean value representing wheter or not the file is available
        /// </summary>
        /// <remarks>
        /// Files are unavailable if they are either deprecated or the addon is not available (<seealso cref="AddonFile.Available"/>)
        /// </remarks>
        public bool Available { get => WrappedType.isAvailable; }

        /// <summary>
        /// Implicitly converts a <see cref="AddonFile"/> into <see cref="AddonFileLite"/>
        /// </summary>
        /// <param name="file">The file to convert from</param>
        public static implicit operator AddonFileLite(AddonFile file)
        {
            var lite = new CurseJSON.GameVersionLatestFile
            {
                fileType = file.WrappedType.releaseType,
                gameVersion = file.Versions.FirstOrDefault(),
                projectFileId = file.Identifier,
                projectFileName = file.FileName
            };

            return new AddonFileLite(lite, file.Client);
        }
    }
}