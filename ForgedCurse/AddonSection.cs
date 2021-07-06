using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForgedCurse
{
    /// <summary>
    /// A section class containing method to retrieve information for Addons
    /// </summary>
    public sealed class AddonSection
    {
        /// <summary>
        /// The base <see cref="ForgeClient"/>
        /// </summary>
        public ForgeClient Client { get; }

        internal AddonSection(ForgeClient client)
        {
            Client = client;
        }
    }
}
