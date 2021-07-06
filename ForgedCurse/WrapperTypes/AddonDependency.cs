using System;
using System.Collections.Generic;
using System.Text;

namespace ForgedCurse.WrapperTypes
{
    /// <summary>
    /// Wrapper around the <see cref="CurseJSON.Dependency"/> class
    /// </summary>
    public class AddonDependency : ForgeWrapper<CurseJSON.Dependency>
    {
        public AddonDependency(CurseJSON.Dependency dependency, ForgeClient client) : base(dependency, client)
        {
        }

        /// <summary>
        /// The identifier for this <see cref="AddonDependency"/>
        /// </summary>
        public int Identifier { get => WrappedType.id; }

        /// <summary>
        /// The identifier to the project that this dependency represents
        /// </summary>
        /// <remarks>
        /// For example, When a file depends on the mod JEI (238222), this will be the id of that mod, 238222
        /// </remarks>
        public int ProjectIdentifier { get => WrappedType.addonId; }

        /// <summary>
        /// The identifier the file that this dependency is associated with
        /// </summary>
        public int FileIdentifier { get => WrappedType.fileId; }
    }
}
