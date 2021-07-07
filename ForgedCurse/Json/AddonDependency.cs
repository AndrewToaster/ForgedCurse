using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForgedCurse.Enumeration;

namespace ForgedCurse.Json
{
    public class AddonDependency
    {
        /// <summary>
        /// The identifier of the addon
        /// </summary>
        public int AddonId { get; set; }

        /// <summary>
        /// The type of this dependency
        /// </summary>
        public DependencyType Type { get; set; }
    }
}
