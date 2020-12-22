using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.ForgeVersion"/> class
    /// </summary>
    public class ForgeVersion : ForgeWrapper<CurseJSON.ForgeVersion>
    {
        public ForgeVersion(CurseJSON.ForgeVersion version, ForgeClient client) : base(version, client)
        {
        }

        /// <summary>
        /// The name of this FML version (e.g. 'forge-14.23.5.2838')
        /// </summary>
        public string Name { get => WrappedType.name; }

        /// <summary>
        /// The version of Minecraft associated with this FML version
        /// </summary>
        public string GameVersion { get => WrappedType.gameVersion; }

        /// <summary>
        /// The time where this FML version was last modified / updated
        /// </summary>
        /// <remarks>
        /// This is probably the date this FML version was released
        /// </remarks>
        public DateTime ModifiedAt { get => WrappedType.dateModified; }

        /// <summary>
        /// Whether or not this FML version has been tested and proved as stable
        /// </summary>
        public bool Recommended { get => WrappedType.recommended; }

        /// <summary>
        /// Whether or not this FML version is the newest version released for this <see cref="GameVersion"/>
        /// </summary>
        public bool Latest { get => WrappedType.latest; }
    }
}
