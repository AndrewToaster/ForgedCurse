using ForgedCurse.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.MinecraftVersion"/> class
    /// </summary>
    public class MinecraftVersion : ForgeWrapper<CurseJSON.MinecraftVersion>
    {
        public MinecraftVersion(CurseJSON.MinecraftVersion version, ForgeClient client) : base(version, client)
        {
        }

        /// <summary>
        /// Gets the identifier of this MC version
        /// </summary>
        public int Identifier { get => WrappedType.gameVersionId; }

        /// <summary>
        /// The string representation of the version (e.g. '1.12.2')
        /// </summary>
        public string Version { get => WrappedType.versionString; }

        /// <summary>
        /// The time where this MC version was last modified / updated
        /// </summary>
        /// <remarks>
        /// This is probably the date this MC version was released. As of 22/12/2020 all versions '1.12.2' and below have the date '2018-03-29T17:24:38.393Z'
        /// </remarks>
        public DateTime ModifiedAt { get => WrappedType.dateModified; }

        /// <summary>
        /// The URL to download the JAR file for this MC version
        /// </summary>
        public string JarDownloadUrl { get => WrappedType.jarDownloadUrl; }

        /// <summary>
        /// The URL to download the JSON file for this MC version
        /// </summary>
        public string JsonDownloadUrl { get => WrappedType.jsonDownloadUrl; }

        /// <summary>
        /// The <see cref="Enumeration.ReleaseType"/> for this MC version
        /// </summary>
        public ReleaseType ReleaseType { get => (ReleaseType)WrappedType.gameVersionStatus; }
    }
}
